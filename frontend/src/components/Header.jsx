import { AppBar, Toolbar, Typography, Button, Box } from "@mui/material";
import { useAuth } from "../auth/useAuth";

export default function Header() {
  const { user, logout } = useAuth();

  return (
    <AppBar position="static">
      <Toolbar sx={{ justifyContent: "space-between" }}>
        <Typography variant="h6">EmpTrack</Typography>

        <Box display="flex" alignItems="center" gap={2}>
          <Typography variant="body1">{user?.username}</Typography>

          <Button
            color="inherit"
            variant="outlined"
            size="small"
            onClick={logout}
          >
            Logout
          </Button>
        </Box>
      </Toolbar>
    </AppBar>
  );
}
