import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { Grid, Button, TextField, Typography } from "@mui/material";

export default function Login() {
  const [name, setName] = useState("");
  const [error, setError] = useState("");
  const navigate = useNavigate();

  const handleLogin = () => {
    const trimmed = name.trim();
    if (trimmed.length < 3) {
      setError("Min 3 letters");
      return;
    }
    if (!/^[a-zA-Z]+$/.test(trimmed)) {
      setError("The name can only contain English letters");
      return;
    }

    localStorage.setItem("investment_username", trimmed);
    navigate("/dashboard");
  };

  return (
    <Grid
      container
      sx={{
        height: "100vh",
        width: "100vw",
        display: "flex",
        flexDirection: "column",
        justifyContent: "center",
        alignItems: "center",
        position: "absolute",
        top: 0,
        left: 0,
        textAlign: "center",
      }}
    >
      <Typography variant="h4" sx={{ mb: 3 }}>
        Investment Simulator
      </Typography>

      <TextField
        label="Name"
        value={name}
        onChange={(e) => setName(e.target.value)}
        error={!!error}
        helperText={error}
        sx={{ mb: 2, width: "300px" }}
      />

      <Button variant="contained" onClick={handleLogin} sx={{ width: "300px" }}>
        Login
      </Button>
    </Grid>
  );
}
