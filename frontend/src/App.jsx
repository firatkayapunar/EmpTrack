import { Routes, Route } from "react-router-dom";
import { paths } from "./routes/paths";
import ProtectedRoute from "./routes/ProtectedRoute";
import AppLayout from "./layouts/AppLayout";
import Login from "./pages/Login";
import Dashboard from "./pages/Dashboard";
import Departments from "./pages/Departments";
import Titles from "./pages/Titles";
import Employees from "./pages/Employees";
import Offline from "./pages/Offline";

export default function App() {
  return (
    <Routes>
      <Route path={paths.login} element={<Login />} />

      <Route element={<ProtectedRoute />}>
        <Route element={<AppLayout />}>
          <Route path={paths.dashboard} element={<Dashboard />} />
          <Route path={paths.departments} element={<Departments />} />
          <Route path={paths.titles} element={<Titles />} />
          <Route path={paths.employees} element={<Employees />} />
        </Route>
      </Route>

      <Route path="/offline" element={<Offline />} />
    </Routes>
  );
}
