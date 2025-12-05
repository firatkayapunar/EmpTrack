 export const parseApiError = (error) => {
  const data = error?.response?.data;

  if (Array.isArray(data?.errors)) {
    return data.errors.join("\n");
  }

  if (data?.errors && typeof data.errors === "object") {
    return Object.values(data.errors).flat().join("\n");
  }

  if (data?.message) {
    return data.message;
  }

  if (error?.message) {
    return error.message;
  }

  return "An unexpected error occurred.";
};
