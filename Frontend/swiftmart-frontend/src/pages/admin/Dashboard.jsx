import React from "react";
import { Link } from "react-router-dom";

export default function AdminDashboard() {
  return (
    <div className="container my-5">
      <h4>Admin Dashboard</h4>
      <ul>
        <li><Link to="/admin/users">Manage Users</Link></li>
        <li><Link to="/admin/sellers">Manage Sellers</Link></li>
        <li><Link to="/admin/categories">Manage Categories</Link></li>
        <li><Link to="/admin/reports">View Reports</Link></li>
      </ul>
    </div>
  );
}
