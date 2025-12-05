import { useEffect, useMemo, useState } from "react";
import {
  Typography,
  Button,
  Table,
  TableHead,
  TableRow,
  TableCell,
  TableBody,
  Avatar,
  Box,
  TextField,
  TablePagination,
  ToggleButtonGroup,
  ToggleButton,
  Chip,
} from "@mui/material";
import Loader from "../components/Loader";
import EmployeeForm from "../components/forms/EmployeeForm";
import ConfirmDialog from "../components/ConfirmDialog";
import { getEmployees, deleteEmployee } from "../services/employee.service";
import useLookups from "../hooks/useLookups";

export default function Employees() {
  const BASE_URL = import.meta.env.VITE_API_BASE_URL;

  const { departments, titles, loading } = useLookups();

  const canAddEmployee =
    !loading && departments.length > 0 && titles.length > 0;

  const [items, setItems] = useState(null);
  const [openForm, setOpenForm] = useState(false);
  const [editing, setEditing] = useState(null);

  const [deleteOpen, setDeleteOpen] = useState(false);
  const [selected, setSelected] = useState(null);

  const [search, setSearch] = useState("");
  const [statusFilter, setStatusFilter] = useState("all");

  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(5);

  const reload = async () => {
    try {
      setItems(await getEmployees());
    } catch {
      setItems([]);
    }
  };

  useEffect(() => {
    reload();
  }, []);

  useEffect(() => {
    if (!items) return;

    const maxPage = Math.max(0, Math.ceil(items.length / rowsPerPage) - 1);

    if (page > maxPage) {
      setPage(maxPage);
    }
  }, [items, rowsPerPage, page]);

  const filteredItems = useMemo(() => {
    if (!items) return [];

    let data = [...items];

    if (search.trim()) {
      const s = search.toLowerCase();
      data = data.filter(
        (e) =>
          e.fullName?.toLowerCase().includes(s) ||
          e.departmentName?.toLowerCase().includes(s) ||
          e.titleName?.toLowerCase().includes(s)
      );
    }

    if (statusFilter === "active") data = data.filter((e) => e.isActive);
    if (statusFilter === "passive") data = data.filter((e) => !e.isActive);

    return data;
  }, [items, search, statusFilter]);

  const pagedItems = useMemo(() => {
    const start = page * rowsPerPage;
    return filteredItems.slice(start, start + rowsPerPage);
  }, [filteredItems, page, rowsPerPage]);

  if (!items || loading) return <Loader />;

  const openCreate = () => {
    if (!canAddEmployee) return;

    setEditing(null);
    setOpenForm(true);
  };

  const openEdit = (e) => {
    const [firstName = "", lastName = ""] = e.fullName?.split(" ") || [];

    const dept = departments.find((d) => d.name === e.departmentName);
    const title = titles.find((t) => t.name === e.titleName);

    setEditing({
      id: e.id,
      registrationNumber: e.registrationNumber,
      firstName,
      lastName,
      departmentId: String(dept?.id || ""),
      titleId: String(title?.id || ""),
      startDate: e.startDate?.slice(0, 10),
      isActive: e.isActive,
      photoPath: e.photoPath,
    });

    setOpenForm(true);
  };

  const askDelete = (item) => {
    setSelected(item);
    setDeleteOpen(true);
  };

  const confirmDelete = async () => {
    if (!selected) return;

    try {
      await deleteEmployee(selected.id);
      reload();
    } finally {
      setDeleteOpen(false);
      setSelected(null);
    }
  };

  return (
    <>
      <Box display="flex" justifyContent="space-between" mb={2}>
        <Typography variant="h4">Employees</Typography>

        <Button
          variant="contained"
          disabled={!canAddEmployee}
          title={
            !canAddEmployee ? "Create at least 1 Department & Title first" : ""
          }
          onClick={openCreate}
        >
          ADD EMPLOYEE
        </Button>
      </Box>

      <Box display="flex" gap={2} mb={2}>
        <TextField
          size="small"
          label="Search name / dept / title"
          value={search}
          onChange={(e) => setSearch(e.target.value)}
        />

        <ToggleButtonGroup
          size="small"
          exclusive
          value={statusFilter}
          onChange={(_, v) => v && setStatusFilter(v)}
        >
          <ToggleButton value="all">All</ToggleButton>
          <ToggleButton value="active">Active</ToggleButton>
          <ToggleButton value="passive">Passive</ToggleButton>
        </ToggleButtonGroup>
      </Box>

      <Table>
        <TableHead>
          <TableRow>
            <TableCell>Photo</TableCell>
            <TableCell>First Name</TableCell>
            <TableCell>Last Name</TableCell>
            <TableCell>Registration Number</TableCell>
            <TableCell>Department</TableCell>
            <TableCell>Title</TableCell>
            <TableCell>Start Date</TableCell>
            <TableCell>Status</TableCell>
            <TableCell width={180}>Actions</TableCell>
          </TableRow>
        </TableHead>

        <TableBody>
          {pagedItems.map((e) => {
            const [firstName, lastName] = e.fullName?.split(" ") || [];

            return (
              <TableRow key={e.id}>
                <TableCell>
                  <Avatar
                    src={e.photoPath ? `${BASE_URL}${e.photoPath}` : undefined}
                  />
                </TableCell>

                <TableCell>{firstName}</TableCell>
                <TableCell>{lastName}</TableCell>
                <TableCell>{e.registrationNumber}</TableCell>
                <TableCell>{e.departmentName}</TableCell>
                <TableCell>{e.titleName}</TableCell>

                <TableCell>
                  {e.startDate
                    ? new Date(e.startDate).toLocaleDateString()
                    : "-"}
                </TableCell>

                <TableCell>
                  <Chip
                    label={e.isActive ? "Active" : "Passive"}
                    color={e.isActive ? "success" : "default"}
                    size="small"
                  />
                </TableCell>

                <TableCell>
                  <Button size="small" onClick={() => openEdit(e)}>
                    EDIT
                  </Button>

                  <Button
                    size="small"
                    color="error"
                    onClick={() => askDelete(e)}
                  >
                    DELETE
                  </Button>
                </TableCell>
              </TableRow>
            );
          })}
        </TableBody>
      </Table>

      <TablePagination
        component="div"
        count={filteredItems.length}
        page={page}
        rowsPerPage={rowsPerPage}
        rowsPerPageOptions={[5, 10, 25]}
        onPageChange={(_, p) => setPage(p)}
        onRowsPerPageChange={(e) => {
          setPage(0);
          setRowsPerPage(parseInt(e.target.value, 10));
        }}
      />

      <EmployeeForm
        open={openForm}
        onClose={() => setOpenForm(false)}
        initialData={editing}
        onSubmitSuccess={reload}
      />

      <ConfirmDialog
        open={deleteOpen}
        title="Delete Employee"
        description={`Delete ${selected?.fullName}?`}
        onConfirm={confirmDelete}
        onCancel={() => setDeleteOpen(false)}
      />
    </>
  );
}
