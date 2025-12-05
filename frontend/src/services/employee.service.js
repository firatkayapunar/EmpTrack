import httpClient from "../api/httpClient";
import { normalizeResponse } from "../utils/apiResponse";

export async function getEmployees() {
  const response = await httpClient.get("/api/employees");
  return normalizeResponse(response);
}

export async function getEmployeeById(id) {
  const response = await httpClient.get(`/api/employees/${id}`);
  return normalizeResponse(response);
}

export async function createEmployee(payload) {
  const response = await httpClient.post("/api/employees", payload);
  return normalizeResponse(response);
}

export async function updateEmployee(id, payload) {
  const response = await httpClient.put(`/api/employees/${id}`, payload);
  return normalizeResponse(response);
}

export async function deleteEmployee(id) {
  const response = await httpClient.delete(`/api/employees/${id}`);
  return normalizeResponse(response);
}

export async function uploadEmployeePhoto(employeeId, file) {
  const formData = new FormData();
  formData.append("photo", file);

  const response = await httpClient.post(
    `/api/employees/${employeeId}/photo`,
    formData,
    {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    }
  );
  return normalizeResponse(response);
}
