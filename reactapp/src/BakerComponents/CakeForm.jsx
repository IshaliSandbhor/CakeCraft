import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import api from "../apiConfig";
export default function CakeForm() {
  const { id } = useParams(),
    nav = useNavigate(),
    [f, setF] = useState({
      name: "",
      category: "Birthday",
      price: "",
      quantity: "",
      cakeImage: "",
    }),
    [imagePreview, setImagePreview] = useState(null);
  useEffect(() => {
    if (id) api.get("/api/cakes/" + id).then((r) => {
      setF(r.data);
      if (r.data.cakeImage) setImagePreview(r.data.cakeImage);
    });
  }, [id]);
  const change = (e) => setF({ ...f, [e.target.name]: e.target.value });
  const handleImageChange = (e) => {
    const file = e.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = (event) => {
        // Create canvas to compress image
        const img = new Image();
        img.onload = () => {
          const canvas = document.createElement("canvas");
          const maxWidth = 400;
          const maxHeight = 400;
          let width = img.width;
          let height = img.height;

          if (width > height) {
            if (width > maxWidth) {
              height *= maxWidth / width;
              width = maxWidth;
            }
          } else {
            if (height > maxHeight) {
              width *= maxHeight / height;
              height = maxHeight;
            }
          }

          canvas.width = width;
          canvas.height = height;
          const ctx = canvas.getContext("2d");
          ctx.drawImage(img, 0, 0, width, height);
          const compressedDataUrl = canvas.toDataURL("image/jpeg", 0.7);
          setF({ ...f, cakeImage: compressedDataUrl });
          setImagePreview(compressedDataUrl);
        };
        img.src = event.target.result;
      };
      reader.readAsDataURL(file);
    }
  };
  async function save(e) {
    e.preventDefault();
    const payload = {
      ...f,
      price: Number(f.price),
      quantity: Number(f.quantity),
    };

    if (id) await api.put("/api/cakes/" + id, payload);
    else await api.post("/api/cakes", payload);

    alert(id ? "Cake updated successfully" : "Cake added successfully");
    nav("/baker/cakes");
  }
  return (
    <main className="center">
      <form className="card" onSubmit={save}>
        <h2>{id ? "Edit" : "Create New"} Cake</h2>
        <input
          name="name"
          placeholder="Name *"
          required
          value={f.name}
          onChange={change}
        />
        <select name="category" value={f.category} onChange={change}>
          <option>Birthday</option>
          <option>Wedding</option>
          <option>Anniversary</option>
          <option>Chocolate</option>
        </select>
        <input
          name="price"
          type="number"
          min="0.01"
          step="0.01"
          placeholder="Price *"
          required
          value={f.price}
          onChange={change}
        />
        <input
          name="quantity"
          type="number"
          min="0.01"
          step="0.01"
          placeholder="Quantity *"
          required
          value={f.quantity}
          onChange={change}
        />
        <input
          type="file"
          accept="image/*"
          onChange={handleImageChange}
        />
        {imagePreview && (
          <div style={{ marginTop: "12px", textAlign: "center" }}>
            <img
              src={imagePreview}
              alt="Preview"
              style={{ maxWidth: "100%", maxHeight: "200px", borderRadius: "8px" }}
            />
          </div>
        )}
        <button>{id ? "Update Cake" : "Add Cake"}</button>
        <button type="button" onClick={() => nav(-1)}>
          Back
        </button>
      </form>
    </main>
  );
}
