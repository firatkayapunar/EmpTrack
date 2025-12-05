import { Card, Box, Typography } from "@mui/material";

export default function StatCard({ title, value, color }) {
  return (
    <Card
      sx={{
        minWidth: 200,
        borderRadius: 3,
        background: `linear-gradient(135deg, ${color.light}, ${color.dark})`,
        color: "#fff",
        boxShadow: "0 10px 25px rgba(0,0,0,0.15)",
        transition: "all .25s ease",
        "&:hover": {
          transform: "translateY(-4px)",
          boxShadow: "0 15px 35px rgba(0,0,0,0.25)",
        },
      }}
    >
      <Box p={3}>
        <Typography variant="body2" sx={{ opacity: 0.9 }}>
          {title}
        </Typography>

        <Typography variant="h3" fontWeight="bold">
          {value}
        </Typography>
      </Box>
    </Card>
  );
}
