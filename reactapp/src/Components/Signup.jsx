import React, { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import api from "../apiConfig";
export default function Signup() {
  const [f, setF] = useState({
      username: "",
      email: "",
      mobileNumber: "",
      password: "",
      confirm: "",
      userRole: "Customer",
    }),
    [e, setE] = useState(""),
    nav = useNavigate();
  const change = (x) => setF({ ...f, [x.target.name]: x.target.value });
  async function submit(x) {
    x.preventDefault();
    if (f.password !== f.confirm) return setE("Passwords do not match");
    try {
      await api.post("/api/register", f);
      alert("User Registration is Successful!");
      nav("/");
    } catch (y) {
      setE(y.response?.data?.message || "Registration failed");
    }
  }
  return (
    <main className="center">
      <form className="card" onSubmit={submit}>
        <h2>Signup</h2>
        {e && <p className="error">{e}</p>}
        <input
          name="username"
          placeholder="User Name *"
          required
          maxLength="30"
          onChange={change}
        />
        <input
          name="email"
          type="email"
          placeholder="Email *"
          required
          onChange={change}
        />
        <input
          name="mobileNumber"
          placeholder="Mobile Number *"
          required
          pattern="[0-9]{10}"
          onChange={change}
        />
        <input
          name="password"
          type="password"
          placeholder="Password *"
          required
          minLength="6"
          onChange={change}
        />
        <input
          name="confirm"
          type="password"
          placeholder="Confirm Password *"
          required
          onChange={change}
        />
        <select name="userRole" onChange={change}>
          <option>Customer</option>
          <option>Baker</option>
        </select>
        <button>Submit</button>
        <Link to="/">Login</Link>
      </form>
    </main>
  );
}
