import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../apiConfig";

export default function BakerOrders() {
  const [orders, setOrders] = useState([]);
  const nav = useNavigate();

  useEffect(() => {
    load();
  }, []);

  async function load() {
    try {
      const result = await api.get("/api/orders");
      setOrders(result.data || []);
    } catch (err) {
      alert("Error loading orders: " + (err.response?.data?.message || err.message));
    }
  }

  async function updateStatus(orderId, status) {
    try {
      await api.put(`/api/orders/${orderId}/status`, { status });
      alert("Order status updated");
      load();
    } catch (err) {
      alert("Error updating status: " + (err.response?.data?.message || err.message));
    }
  }

  return (
    <main>
      <div style={{ display: "flex", gap: "12px", alignItems: "center", marginBottom: "16px" }}>
        <button onClick={() => nav("/home")}>Back</button>
        <h2 style={{ margin: 0 }}>Customer Orders</h2>
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
                <strong>Customer:</strong> {order.customerName}
              </p>
              <p>
                <strong>Quantity:</strong> {order.quantity}
              </p>
              <p>
                <strong>Total Price:</strong> ₹{order.totalPrice}
              </p>
              <p>
                <strong>Status:</strong> {order.status}
              </p>
              <p style={{ fontSize: "0.9em", color: "#666" }}>
                Ordered: {new Date(order.createdAt).toLocaleString()}
              </p>
              <div style={{ marginTop: "12px", display: "flex", gap: "8px" }}>
                <button onClick={() => updateStatus(order.orderId, "Confirmed")}>Confirm</button>
                <button onClick={() => updateStatus(order.orderId, "Completed")}>Complete</button>
                <button onClick={() => updateStatus(order.orderId, "Cancelled")}>Cancel</button>
              </div>
            </article>
          ))}
        </section>
      ) : (
        <p>No orders yet</p>
      )}
    </main>
  );
}
