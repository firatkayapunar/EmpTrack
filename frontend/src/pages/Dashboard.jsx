import { useEffect, useState } from "react";
import { Grid, Typography } from "@mui/material";
import Loader from "../components/Loader";
import StatCard from "../components/StatCard";
import { getDashboardStats } from "../services/dashboard.service";
import { notification } from "../utils/notification";

export default function Dashboard() {
  const [stats, setStats] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    async function load() {
      try {
        const result = await getDashboardStats();
        setStats(result);
      } catch (error) {
        const apiMessage =
          error?.response?.data?.message ||
          error?.response?.data?.error ||
          "Dashboard data could not be loaded.";

        notification.error(apiMessage);
      } finally {
        setLoading(false);
      }
    }

    load();
  }, []);

  if (loading || !stats) return <Loader />;

  return (
    <>
      <Typography variant="h4" mb={3}>
        Dashboard
      </Typography>

      <Grid container spacing={3}>
        <Grid item>
          <StatCard
            title="Total Employees"
            value={stats.totalEmployees}
            color={{ light: "#1e88e5", dark: "#1565c0" }}
          />
        </Grid>

        <Grid item>
          <StatCard
            title="Total Departments"
            value={stats.totalDepartments}
            color={{ light: "#43a047", dark: "#2e7d32" }}
          />
        </Grid>

        <Grid item>
          <StatCard
            title="Total Titles"
            value={stats.totalTitles}
            color={{ light: "#fb8c00", dark: "#ef6c00" }}
          />
        </Grid>

        <Grid item>
          <StatCard
            title="Active Employees"
            value={stats.activeEmployees}
            color={{ light: "#8e24aa", dark: "#6a1b9a" }}
          />
        </Grid>

        <Grid item>
          <StatCard
            title="Passive Employees"
            value={stats.passiveEmployees}
            color={{ light: "#e53935", dark: "#b71c1c" }}
          />
        </Grid>
      </Grid>
    </>
  );
}
