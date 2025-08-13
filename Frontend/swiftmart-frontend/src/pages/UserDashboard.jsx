import React from "react";
import { Link } from "react-router-dom";

export default function UserDashboard() {
  return (
    <div className="container my-5">
      <h4>User Dashboard</h4>
      <ul>
        <li><Link to="/products">Browse Products</Link></li>
        <li><Link to="/cart">My Cart</Link></li>
        <li><Link to="/orders">My Orders</Link></li>
      </ul>
    </div>
  );
}
