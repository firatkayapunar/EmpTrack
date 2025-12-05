import httpClient from "../api/httpClient";
import { normalizeResponse } from "../utils/apiResponse";

export async function getTitles() {
  const response = await httpClient.get("/api/titles");
  return normalizeResponse(response);
}

export async function getTitleById(id) {
  const response = await httpClient.get(`/api/titles/${id}`);
  return normalizeResponse(response);
}

export async function createTitle(payload) {
  const response = await httpClient.post("/api/titles", payload);
  return normalizeResponse(response);
}

export async function updateTitle(id, payload) {
  const response = await httpClient.put(`/api/titles/${id}`, payload);
  return normalizeResponse(response);
}
