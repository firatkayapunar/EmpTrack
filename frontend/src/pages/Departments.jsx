import { useEffect, useState } from "react";
import {
  Typography,
  Button,
  Table,
  TableHead,
  TableRow,
  TableCell,
  TableBody,
} from "@mui/material";
import Loader from "../components/Loader";
import DepartmentForm from "../components/forms/DepartmentForm";
import { getDepartments } from "../services/department.service";
import { notification } from "../utils/notification";

export default function Departments() {
  const [items, setItems] = useState(null);
  const [openForm, setOpenForm] = useState(false);
  const [editing, setEditing] = useState(null);

  const loadDepartments = async () => {
    try {
      const result = await getDepartments();
      setItems(result || []);
    } catch (error) {
      const apiMessage =
        error?.response?.data?.message ||
        error?.response?.data?.error ||
        "Departments could not be loaded.";

      notification.error(apiMessage);
      setItems([]);
    }
  };

  useEffect(() => {
    (async () => {
      await loadDepartments();
    })();
  }, []);

  const openCreate = () => {
    setEditing(null);
    setOpenForm(true);
  };

  const openEdit = (item) => {
    setEditing(item);
    setOpenForm(true);
  };

  if (!items) return <Loader />;

  return (
    <>
      <Typography variant="h4" mb={2}>
        Departments
      </Typography>

      <Button variant="contained" onClick={openCreate} sx={{ mb: 2 }}>
        Add Department
      </Button>

      <Table>
        <TableHead>
          <TableRow>
            <TableCell>Name</TableCell>
            <TableCell>Description</TableCell>
            <TableCell width={120}>Actions</TableCell>
          </TableRow>
        </TableHead>

        <TableBody>
          {items.map((d) => (
            <TableRow key={d.id}>
              <TableCell>{d.name}</TableCell>
              <TableCell>{d.description}</TableCell>
              <TableCell>
                <Button size="small" onClick={() => openEdit(d)}>
                  Edit
                </Button>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>

      <DepartmentForm
        open={openForm}
        onClose={() => setOpenForm(false)}
        initialData={editing}
        onSubmitSuccess={loadDepartments}
      />
    </>
  );
}
