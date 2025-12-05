import { Box, CircularProgress } from "@mui/material";

export default function Loader({ fullScreen = false }) {
  if (fullScreen) {
    return (
      <Box
        display="flex"
        alignItems="center"
        justifyContent="center"
        minHeight="100vh"
      >
        <CircularProgress />
      </Box>
    );
  }

  return (
    <Box display="flex" alignItems="center" justifyContent="center" py={3}>
      <CircularProgress size={36} />
    </Box>
  );
}
