import httpClient from "../api/httpClient";
import { normalizeResponse } from "../utils/apiResponse";

export async function getDashboardStats() {
  const response = await httpClient.get("/api/dashboard/stats");

  return normalizeResponse(response);
}
