import { Box, Typography, Button } from "@mui/material";

export default function Offline() {
  const handleRetry = () => {
    window.location.href = "/";
  };

  return (
    <Box
      height="100vh"
      display="flex"
      flexDirection="column"
      alignItems="center"
      justifyContent="center"
      gap={3}
      sx={{
        background: "linear-gradient(135deg,#0B1220,#111827)",
        color: "#fff",
        textAlign: "center",
      }}
    >
      <Typography variant="h3" fontWeight={700}>
        ⚠️ Service Unavailable
      </Typography>

      <Typography variant="body1" sx={{ opacity: 0.85 }}>
        Backend services are currently unreachable.
        <br />
        Please try again later.
      </Typography>

      <Button
        variant="contained"
        onClick={handleRetry}
        sx={{ borderRadius: 3, mt: 1 }}
      >
        Please Try Again
      </Button>
    </Box>
  );
}
