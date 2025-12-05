import { Button } from "@mui/material";

export default function PhotoUpload({ onSelect }) {
  return (
    <Button variant="outlined" component="label" sx={{ mt: 1 }}>
      Upload Photo
      <input
        hidden
        type="file"
        accept="image/*"
        onChange={(e) => onSelect(e.target.files[0])}
      />
    </Button>
  );
}
