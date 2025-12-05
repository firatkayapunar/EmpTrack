import { Navigate, Outlet } from "react-router-dom";
import { useAuth } from "../auth/useAuth";
import { paths } from "./paths";
import Loader from "../components/Loader";

export default function ProtectedRoute() {
  const { isAuthenticated, isLoading } = useAuth();

  if (isLoading) {
    return <Loader fullScreen />;
  }

  if (!isAuthenticated()) {
    return <Navigate to={paths.login} replace />;
  }

  return <Outlet />;
}
