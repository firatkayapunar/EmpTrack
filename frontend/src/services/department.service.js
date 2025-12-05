import httpClient from "../api/httpClient";
import { normalizeResponse } from "../utils/apiResponse";

export async function getDepartments() {
  const response = await httpClient.get("/api/departments");
  return normalizeResponse(response);
}

export async function getDepartmentById(id) {
  const response = await httpClient.get(`/api/departments/${id}`);
  return normalizeResponse(response);
}

export async function createDepartment(payload) {
  const response = await httpClient.post("/api/departments", payload);
  return normalizeResponse(response);
}

export async function updateDepartment(id, payload) {
  const response = await httpClient.put(`/api/departments/${id}`, payload);
  return normalizeResponse(response);
}
