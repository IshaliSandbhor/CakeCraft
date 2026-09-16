import React from "react";
import { useNavigate } from "react-router-dom";
export default function HomePage() {
  const nav = useNavigate(),
    role = localStorage.getItem("role");
  function logout() {
    localStorage.clear();
    nav("/");
  }
  return (
    <>
      <nav>
        <b>CakeCraft</b>
        <span>
          {localStorage.getItem("username")} / {role}
        </span>
        <button
          onClick={() =>
            nav(role === "Baker" ? "/baker/cakes" : "/customer/cakes")
          }
        >
          Cakes
        </button>
        <button
          onClick={() =>
            nav(role === "Baker" ? "/baker/orders" : "/customer/orders")
          }
        >
          Orders
        </button>
        <button onClick={logout}>Logout</button>
      </nav>
      <main className="hero">
        <h1>CakeCraft</h1>
        <p>Browse and manage handcrafted cakes for every celebration.</p>
      </main>
    </>
  );
}
