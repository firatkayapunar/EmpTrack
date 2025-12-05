import { enqueueSnackbar } from "notistack";

export const notification = {
  success(message) {
    enqueueSnackbar(message, {
      variant: "success",
      autoHideDuration: 3000,
    });
  },

  error(message) {
    enqueueSnackbar(message || "Unexpected error occurred.", {
      variant: "error",
      autoHideDuration: 4000,
    });
  },

  info(message) {
    enqueueSnackbar(message, {
      variant: "info",
      autoHideDuration: 3000,
    });
  },

  warning(message) {
    enqueueSnackbar(message, {
      variant: "warning",
      autoHideDuration: 3500,
    });
  },
};
