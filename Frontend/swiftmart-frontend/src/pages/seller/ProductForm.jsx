import React from "react";

export default function ProductForm() {
  return (
    <div className="container my-5">
      <h4>Add New Product</h4>
      <form>
        <input className="form-control mb-2" placeholder="Product Name"/>
        <input className="form-control mb-2" placeholder="Price"/>
        <textarea className="form-control mb-2" placeholder="Description"></textarea>
        <button className="btn btn-primary">Save</button>
      </form>
    </div>
  );
}
