import { Box } from "@mui/material";
import { Outlet } from "react-router-dom";
import Header from "../components/Header";
import Sidebar from "../components/Sidebar";

export default function AppLayout() {
  return (
    <Box display="flex" flexDirection="column" height="100vh">
      <Header />

      <Box display="flex" flex={1}>
        <Sidebar />

        <Box
          component="main"
          sx={{
            flexGrow: 1,
            p: 3,
            backgroundColor: "#f5f5f5",
            overflow: "auto",
          }}
        >
          <Outlet />
        </Box>
      </Box>
    </Box>
  );
}
