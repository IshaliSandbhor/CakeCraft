import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../apiConfig";
export default function CustomerViewCake() {
  const [cakes, setCakes] = useState([]),
    [q, setQ] = useState(""),
    nav = useNavigate();

  useEffect(() => {
    api.get("/api/cakes").then((r) => setCakes(r.data));
  }, []);

  const list = cakes.filter((c) =>
    c.name.toLowerCase().includes(q.toLowerCase()),
  );

  async function placeOrder(cake) {
    const qty = window.prompt(`Enter quantity for ${cake.name}`, "1");
    if (qty === null) return;

    const quantity = Number(qty);
    if (!Number.isFinite(quantity) || quantity <= 0) {
      alert("Please enter a valid quantity greater than 0.");
      return;
    }

    try {
      await api.post("/api/orders", {
        cakeId: cake.cakeId,
        quantity,
      });
      alert(`Order placed for ${cake.name}`);
    } catch (err) {
      alert(err.response?.data?.message || "Order failed");
    }
  }

  return (
    <main>
      <div style={{ display: "flex", gap: "12px", alignItems: "center", marginBottom: "16px" }}>
        <button onClick={() => nav("/home")}>Back</button>
        <h2 style={{ margin: 0 }}>Available Cakes</h2>
      </div>
      <input
        placeholder="Search by cake name"
        value={q}
        onChange={(e) => setQ(e.target.value)}
      />
      <section className="grid">
        {list.length ? (
          list.map((c) => (
            <article className="cake" key={c.cakeId}>
              <img
                src={c.cakeImage || "https://placehold.co/300x180?text=Cake"}
                alt={c.name}
              />
              <h3>{c.name}</h3>
              <p>
                {c.category} | ₹{c.price}
              </p>
              <button onClick={() => placeOrder(c)}>Order</button>
            </article>
          ))
        ) : (
          <p>Oops! No cakes found</p>
        )}
      </section>
    </main>
  );
}
