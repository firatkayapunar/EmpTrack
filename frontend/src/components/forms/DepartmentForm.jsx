import { useForm } from "react-hook-form";
import { useEffect } from "react";
import { TextField } from "@mui/material";
import Modal from "../Modal";
import {
  createDepartment,
  updateDepartment,
} from "../../services/department.service";
import { notification } from "../../utils/notification";
import { parseApiError } from "../../utils/apiErrorParser";

export default function DepartmentForm({
  open,
  onClose,
  initialData,
  onSubmitSuccess,
}) {
  const { register, handleSubmit, reset } = useForm({
    defaultValues: {
      name: "",
      description: "",
    },
  });

  useEffect(() => {
    reset(
      initialData || {
        name: "",
        description: "",
      }
    );
  }, [initialData, reset]);

  const submit = async (form) => {
    try {
      if (initialData) {
        await updateDepartment(initialData.id, form);
        notification.success("Department updated successfully.");
      } else {
        await createDepartment(form);
        notification.success("Department created successfully.");
      }

      onSubmitSuccess();
      onClose();
    } catch (error) {
      notification.error(parseApiError(error));
    }
  };

  return (
    <Modal
      open={open}
      onClose={onClose}
      title={initialData ? "Edit Department" : "Add Department"}
      onSubmit={handleSubmit(submit)}
    >
      <TextField
        fullWidth
        label="Department Name"
        margin="normal"
        {...register("name", { required: true })}
      />

      <TextField
        fullWidth
        label="Description"
        margin="normal"
        {...register("description")}
      />
    </Modal>
  );
}
