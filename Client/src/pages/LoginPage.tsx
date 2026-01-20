import { useState } from "react";
import { useNavigate } from "react-router-dom";

export default function Login() {
  const [name, setName] = useState("");
  const [error, setError] = useState("");
  const navigate = useNavigate();

  const handleLogin = () => {
    const trimmed = name.trim();
    if (trimmed.length < 3) {
      setError("The name must be at least 3 characters long");
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
    <div>
      <h1>Investment simulator</h1>
      <input
        value={name}
        onChange={(e) => {
          setName(e.target.value);
          setError("");
        }}
        placeholder="Enter name (in English)"
      />
      {error && <p style={{ color: "red" }}>{error}</p>}
      <button onClick={handleLogin}>login</button>
    </div>
  );
}
