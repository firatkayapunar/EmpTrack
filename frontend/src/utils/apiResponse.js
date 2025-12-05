export const normalizeResponse = (response) => {
  return response?.data?.data ?? response?.data ?? response;
};
