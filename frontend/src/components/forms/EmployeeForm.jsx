import {
  TextField,
  MenuItem,
  Switch,
  FormControlLabel,
  Avatar,
  Box,
  IconButton,
} from "@mui/material";
import OpenInNewIcon from "@mui/icons-material/OpenInNew";
import { useForm, Controller } from "react-hook-form";
import { useEffect, useMemo, useState } from "react";
import Modal from "../Modal";
import PhotoUpload from "../PhotoUpload";
import {
  createEmployee,
  updateEmployee,
  uploadEmployeePhoto,
} from "../../services/employee.service";
import useLookups from "../../hooks/useLookups";
import { notification } from "../../utils/notification";
import { parseApiError } from "../../utils/apiErrorParser";

const BASE_URL = import.meta.env.VITE_API_BASE_URL;

const defaultValues = {
  registrationNumber: "",
  firstName: "",
  lastName: "",
  departmentId: "",
  titleId: "",
  startDate: "",
  isActive: true,
};

export default function EmployeeForm({
  open,
  onClose,
  initialData,
  onSubmitSuccess,
}) {
  const { departments, titles } = useLookups();

  const isCreate = !initialData;

  const { register, handleSubmit, reset, control } = useForm({
    defaultValues,
  });

  const [photo, setPhoto] = useState(null);

  useEffect(() => {
    if (!open) return;

    if (isCreate) {
      if (departments.length && titles.length) {
        reset({
          ...defaultValues,
          departmentId: String(departments[0].id),
          titleId: String(titles[0].id),
        });
      } else {
        reset(defaultValues);
      }
    } else {
      reset({
        ...initialData,
        departmentId: String(initialData.departmentId),
        titleId: String(initialData.titleId),
      });
    }
  }, [open, initialData, departments, titles, reset, isCreate]);

  const previewUrl = useMemo(() => {
    if (photo) return URL.createObjectURL(photo);
    return initialData?.photoPath
      ? `${BASE_URL}${initialData.photoPath}`
      : null;
  }, [photo, initialData]);

  const openPreview = () => previewUrl && window.open(previewUrl, "_blank");

  const handlePhotoSelect = (file) => {
    if (!file) return;

    if (!["image/jpeg", "image/png"].includes(file.type)) {
      return notification.warning("Only JPG or PNG images are allowed.");
    }

    if (file.size > 5 * 1024 * 1024) {
      return notification.warning("Maximum file size is 5MB.");
    }

    setPhoto(file);
  };

  const submit = async (form) => {
    try {
      const payload = {
        ...form,
        departmentId: Number(form.departmentId),
        titleId: Number(form.titleId),
      };

      const response = isCreate
        ? await createEmployee(payload)
        : await updateEmployee(initialData.id, payload);

      if (photo && response?.id) {
        await uploadEmployeePhoto(response.id, photo);
      }

      notification.success(
        isCreate
          ? "Employee created successfully."
          : "Employee updated successfully."
      );

      reset(defaultValues);
      setPhoto(null);

      onSubmitSuccess();
      onClose();
    } catch (error) {
      notification.error(parseApiError(error));
    }
  };

  const handleClose = () => {
    reset(defaultValues);
    setPhoto(null);
    onClose();
  };

  return (
    <Modal
      open={open}
      onClose={handleClose}
      title={isCreate ? "Add Employee" : "Edit Employee"}
      onSubmit={handleSubmit(submit)}
    >
      <Box display="flex" gap={2} mb={2}>
        <PhotoUpload onSelect={handlePhotoSelect} />

        {previewUrl && (
          <>
            <Avatar
              src={previewUrl}
              sx={{ width: 56, height: 56 }}
              onClick={openPreview}
            />

            <IconButton onClick={openPreview}>
              <OpenInNewIcon fontSize="small" />
            </IconButton>
          </>
        )}
      </Box>

      <TextField
        fullWidth
        margin="normal"
        label="Registration Number"
        {...register("registrationNumber", { required: true })}
      />

      <TextField
        fullWidth
        margin="normal"
        label="First Name"
        {...register("firstName", { required: true })}
      />

      <TextField
        fullWidth
        margin="normal"
        label="Last Name"
        {...register("lastName", { required: true })}
      />

      <Controller
        name="departmentId"
        control={control}
        rules={{ required: true }}
        render={({ field }) => (
          <TextField
            select
            fullWidth
            margin="normal"
            label="Department"
            {...field}
          >
            {departments.map((d) => (
              <MenuItem key={d.id} value={String(d.id)}>
                {d.name}
              </MenuItem>
            ))}
          </TextField>
        )}
      />

      <Controller
        name="titleId"
        control={control}
        rules={{ required: true }}
        render={({ field }) => (
          <TextField select fullWidth margin="normal" label="Title" {...field}>
            {titles.map((t) => (
              <MenuItem key={t.id} value={String(t.id)}>
                {t.name}
              </MenuItem>
            ))}
          </TextField>
        )}
      />

      <TextField
        type="date"
        fullWidth
        margin="normal"
        label="Start Date"
        InputLabelProps={{ shrink: true }}
        {...register("startDate", { required: true })}
      />

      {!isCreate && (
        <FormControlLabel
          label="Active"
          control={
            <Controller
              name="isActive"
              control={control}
              render={({ field }) => (
                <Switch
                  checked={field.value}
                  onChange={(e) => field.onChange(e.target.checked)}
                />
              )}
            />
          }
        />
      )}
    </Modal>
  );
}
