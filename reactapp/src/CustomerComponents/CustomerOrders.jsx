import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../apiConfig";

export default function CustomerOrders() {
  const [orders, setOrders] = useState([]);
  const nav = useNavigate();

  useEffect(() => {
    load();
  }, []);

  async function load() {
    try {
      const result = await api.get("/api/orders/my-orders");
      setOrders(result.data || []);
    } catch (err) {
      alert("Error loading orders: " + (err.response?.data?.message || err.message));
    }
  }

  return (
    <main>
      <div style={{ display: "flex", gap: "12px", alignItems: "center", marginBottom: "16px" }}>
        <button onClick={() => nav("/home")}>Back</button>
        <h2 style={{ margin: 0 }}>My Orders</h2>
      </div>
      {orders.length ? (
        <section className="grid">
          {orders.map((order) => (
            <article key={order.orderId} className="order-card" style={{ padding: "16px", border: "1px solid #ddd", borderRadius: "8px" }}>
              <h3>{order.cake?.name}</h3>
              <p>
                <strong>Order ID:</strong> {order.orderId}
              </p>
              <p>
                <strong>Quantity:</strong> {order.quantity}
              </p>
              <p>
                <strong>Total Price:</strong> ₹{order.totalPrice}
              </p>
              <p>
                <strong>Status:</strong> <span style={{ fontWeight: "bold", color: order.status === "Completed" ? "green" : order.status === "Cancelled" ? "red" : "orange" }}>{order.status}</span>
              </p>
              <p style={{ fontSize: "0.9em", color: "#666" }}>
                Ordered: {new Date(order.createdAt).toLocaleString()}
              </p>
            </article>
          ))}
        </section>
      ) : (
        <p>No orders yet. <button onClick={() => nav("/customer/cakes")}>Browse cakes</button></p>
      )}
    </main>
  );
}
