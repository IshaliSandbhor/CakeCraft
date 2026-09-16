import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../apiConfig";
export default function ViewCake() {
  const [cakes, setCakes] = useState([]),
    nav = useNavigate();

  const load = async () => {
    const cakesResp = await api.get("/api/cakes");
    setCakes(cakesResp.data);
  };

  useEffect(() => {
    load();
  }, []);

  async function del(id) {
    if (window.confirm("Delete this cake?")) {
      await api.delete("/api/cakes/" + id);
      load();
    }
  }

  return (
    <main>
      <div style={{ display: "flex", gap: "12px", alignItems: "center", marginBottom: "16px" }}>
        <button onClick={() => nav("/home")}>Back</button>
        <h2 style={{ margin: 0 }}>Baker Cakes</h2>
      </div>
      <button onClick={() => nav("/baker/cakes/new")}>Add Cake</button>
      <section className="grid">
        {cakes.length ? (
          cakes.map((c) => (
            <article className="cake" key={c.cakeId}>
              <img
                src={c.cakeImage || "https://placehold.co/300x180?text=Cake"}
                alt={c.name}
              />
              <h3>{c.name}</h3>
              <p>
                {c.category} | ₹{c.price} | Qty {c.quantity}
              </p>
              <button onClick={() => nav("/baker/cakes/" + c.cakeId)}>
                Edit
              </button>
              <button onClick={() => del(c.cakeId)}>Delete</button>
            </article>
          ))
        ) : (
          <p>Oops! No cakes found</p>
        )}
      </section>
    </main>
  );
}
