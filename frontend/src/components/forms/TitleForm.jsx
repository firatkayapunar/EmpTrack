import { useForm } from "react-hook-form";
import { useEffect } from "react";
import { TextField } from "@mui/material";
import Modal from "../Modal";
import { createTitle, updateTitle } from "../../services/title.service";
import { notification } from "../../utils/notification";
import { parseApiError } from "../../utils/apiErrorParser";

export default function TitleForm({
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
        await updateTitle(initialData.id, form);
        notification.success("Title updated successfully.");
      } else {
        await createTitle(form);
        notification.success("Title created successfully.");
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
      title={initialData ? "Edit Title" : "Add Title"}
      onSubmit={handleSubmit(submit)}
    >
      <TextField
        fullWidth
        label="Title Name"
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
