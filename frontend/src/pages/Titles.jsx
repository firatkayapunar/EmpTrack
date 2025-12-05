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
import TitleForm from "../components/forms/TitleForm";
import { getTitles } from "../services/title.service";
import { notification } from "../utils/notification";

export default function Titles() {
  const [items, setItems] = useState(null);
  const [openForm, setOpenForm] = useState(false);
  const [editing, setEditing] = useState(null);

  const loadTitles = async () => {
    try {
      const result = await getTitles();
      setItems(result || []);
    } catch (error) {
      const apiMessage =
        error?.response?.data?.message ||
        error?.response?.data?.error ||
        "Titles could not be loaded";

      notification.error(apiMessage);
      setItems([]);
    }
  };

  useEffect(() => {
    (async () => {
      await loadTitles();
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
        Titles
      </Typography>

      <Button variant="contained" onClick={openCreate} sx={{ mb: 2 }}>
        Add Title
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
          {items.map((t) => (
            <TableRow key={t.id}>
              <TableCell>{t.name}</TableCell>
              <TableCell>{t.description}</TableCell>
              <TableCell>
                <Button size="small" onClick={() => openEdit(t)}>
                  Edit
                </Button>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>

      <TitleForm
        open={openForm}
        onClose={() => setOpenForm(false)}
        initialData={editing}
        onSubmitSuccess={loadTitles}
      />
    </>
  );
}
