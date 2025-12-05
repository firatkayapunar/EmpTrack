import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  Typography,
} from "@mui/material";

export default function ConfirmDialog({
  open,
  title,
  description,
  onConfirm,
  onCancel,
}) {
  if (!open) return null;

  return (
    <Dialog open={open} onClose={onCancel} maxWidth="xs" fullWidth>
      <DialogTitle>{title || "Confirmation"}</DialogTitle>

      <DialogContent>
        <Typography>{description}</Typography>
      </DialogContent>

      <DialogActions>
        <Button onClick={onCancel}>Cancel</Button>

        <Button onClick={onConfirm} variant="contained" color="error">
          Yes, Delete
        </Button>
      </DialogActions>
    </Dialog>
  );
}
