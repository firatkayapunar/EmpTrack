import axios from "axios";
import {
  getAccessToken,
  getRefreshToken,
  setTokens,
  clearTokens,
} from "../utils/tokenStorage";

const BASE_URL = import.meta.env.VITE_API_BASE_URL;

const apiClient = axios.create({
  baseURL: BASE_URL,
  headers: {
    "Content-Type": "application/json",
  },
});

const authApiClient = axios.create({
  baseURL: BASE_URL,
  headers: {
    "Content-Type": "application/json",
  },
});

apiClient.interceptors.request.use(
  (config) => {
    const token = getAccessToken();
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }

    return config;
  },
  (error) => Promise.reject(error)
);

let isRefreshing = false;
let queue = [];

const processQueue = (error, token = null) => {
  queue.forEach((p) => {
    if (error) p.reject(error);
    else p.resolve(token);
  });

  queue = [];
};

apiClient.interceptors.response.use(
  (response) => response,

  async (error) => {
    const originalRequest = error.config;

    if (
      !error.response &&
      (error.code === "ERR_NETWORK" || error.message?.includes("Network Error"))
    ) {
      window.location.href = "/offline";
      return Promise.reject(error);
    }

    const isAuthRequest = originalRequest?.url?.includes("/api/auth");

    if (isAuthRequest) {
      return Promise.reject(error);
    }

    if (
      error.response?.status === 401 &&
      Array.isArray(error.response?.data) &&
      error.response.data.includes("Invalid refresh token.")
    ) {
      clearTokens();
      window.location.href = "/login";
      return Promise.reject(error);
    }

    if (error.response?.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true;

      if (isRefreshing) {
        return new Promise((resolve, reject) => {
          queue.push({ resolve, reject });
        })
          .then((token) => {
            originalRequest.headers.Authorization = `Bearer ${token}`;
            return apiClient(originalRequest);
          })
          .catch(Promise.reject);
      }

      isRefreshing = true;

      try {
        const refreshToken = getRefreshToken();

        if (!refreshToken) {
          clearTokens();
          window.location.href = "/login";
          return Promise.reject(error);
        }

        const response = await authApiClient.post("/api/auth/refresh", {
          refreshToken,
        });

        const newTokens = response.data.data;

        setTokens(newTokens.accessToken, newTokens.refreshToken);

        apiClient.defaults.headers.Authorization = `Bearer ${newTokens.accessToken}`;

        processQueue(null, newTokens.accessToken);

        return apiClient(originalRequest);
      } catch (refreshError) {
        processQueue(refreshError, null);

        clearTokens();
        window.location.href = "/login";

        return Promise.reject(refreshError);
      } finally {
        isRefreshing = false;
      }
    }

    return Promise.reject(error);
  }
);

export default apiClient;
