import { useState, useEffect } from "react";
import { AuthContext } from "./AuthContext";
import {
  loginRequest,
  refreshRequest,
  logoutRequest,
} from "../services/auth.service";
import {
  setTokens,
  clearTokens,
  getAccessToken,
  getRefreshToken,
} from "../utils/tokenStorage";

export function AuthProvider({ children }) {
  const [user, setUser] = useState(null);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const tryRefresh = async () => {
      const refreshToken = getRefreshToken();

      if (!refreshToken) {
        setIsLoading(false);
        return;
      }

      try {
        const response = await refreshRequest(refreshToken);

        if (response?.data) {
          setTokens(response.data.accessToken, response.data.refreshToken);

          setUser({
            username: response.data.username,
          });
        } else {
          clearTokens();
        }
      } catch {
        clearTokens();
      } finally {
        setIsLoading(false);
      }
    };

    tryRefresh();
  }, []);

  const login = async (username, password) => {
    const data = await loginRequest({ username, password });

    if (data?.accessToken && data?.refreshToken) {
      setTokens(data.accessToken, data.refreshToken);

      setUser({
        username: data.username,
      });

      return true;
    }

    throw new Error("Login failed.");
  };

  const logout = async () => {
    try {
      const refreshToken = getRefreshToken();
      if (refreshToken) {
        await logoutRequest(refreshToken);
      }
    } finally {
      setUser(null);
      clearTokens();
    }
  };

  const isAuthenticated = () => {
    return !!getAccessToken() || !!getRefreshToken();
  };

  const value = {
    user,
    login,
    logout,
    isAuthenticated,
    isLoading,
  };

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}
