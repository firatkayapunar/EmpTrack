import httpClient from "../api/httpClient";
import { normalizeResponse } from "../utils/apiResponse";

export async function loginRequest(credentials) {
  const response = await httpClient.post("/api/auth/login", credentials);
  return normalizeResponse(response);
}

export async function refreshRequest(refreshToken) {
  const response = await httpClient.post("/api/auth/refresh", {
    refreshToken,
  });
  return normalizeResponse(response);
}

export async function logoutRequest(refreshToken) {
  const response = await httpClient.post("/api/auth/logout", {
    refreshToken,
  });
  return normalizeResponse(response);
}
