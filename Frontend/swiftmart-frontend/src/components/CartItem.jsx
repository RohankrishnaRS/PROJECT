import React from "react";

export default function CartItem({ item, onRemove, onQuantityChange }) {
  return (
    <div className="d-flex justify-content-between align-items-center border-bottom py-2">
      <div className="d-flex align-items-center">
        <img
          src={item.image || "/logo.png"}
          alt={item.name}
          width="60"
          height="60"
          className="me-2 rounded"
        />
        <div>
          <h6 className="mb-0">{item.name}</h6>
          <small className="text-muted">₹{item.price}</small>
        </div>
      </div>
      <div className="d-flex align-items-center">
        <input
          type="number"
          value={item.quantity}
          min="1"
          className="form-control form-control-sm me-2"
          style={{ width: "70px" }}
          onChange={(e) => onQuantityChange(item.id, e.target.value)}
        />
        <button className="btn btn-sm btn-danger" onClick={() => onRemove(item.id)}>
          <i className="bi bi-trash"></i>
        </button>
      </div>
    </div>
  );
}
