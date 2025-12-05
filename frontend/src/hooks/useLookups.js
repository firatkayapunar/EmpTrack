import { useEffect, useState } from "react";
import { getDepartments } from "../services/department.service";
import { getTitles } from "../services/title.service";
import { notification } from "../utils/notification";
import { parseApiError } from "../utils/apiErrorParser";

export default function useLookups() {
  const [departments, setDepartments] = useState([]);
  const [titles, setTitles] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    let mounted = true;

    const load = async () => {
      try {
        setLoading(true);
        setError(null);

        const [depRes, titleRes] = await Promise.all([
          getDepartments(),
          getTitles(),
        ]);

        if (!mounted) return;

        setDepartments(depRes?.data ?? depRes ?? []);
        setTitles(titleRes?.data ?? titleRes ?? []);
      } catch (error) {
        if (!mounted) return;

        setError("Failed to load lookup data.");
        setDepartments([]);
        setTitles([]);

        notification.error(parseApiError(error));
      } finally {
        if (mounted) setLoading(false);
      }
    };

    load();

    return () => {
      mounted = false;
    };
  }, []);

  return {
    departments,
    titles,
    loading,
    error,
    reload: () => {
      window.location.reload();
    },
  };
}
