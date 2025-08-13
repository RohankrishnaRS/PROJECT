import React from "react";
import { Link } from "react-router-dom";

export default function SellerDashboard() {
  return (
    <div className="container my-5">
      <h4>Seller Dashboard</h4>
      <ul>
        <li><Link to="/seller/products/add">Add Product</Link></li>
        <li><Link to="/seller/orders">View Orders</Link></li>
        <li><Link to="/seller/sales-report">Sales Report</Link></li>
      </ul>
    </div>
  );
}
