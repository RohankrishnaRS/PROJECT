import React, { useEffect, useState } from "react";
import { getAllProducts } from "../api/productApi";   // ✅ FIX
import ProductCard from "../components/ProductCard";
import Loader from "../components/Loader";

export default function ProductList() {
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    getAllProducts()   // ✅ FIX
      .then(setProducts)
      .finally(() => setLoading(false));
  }, []);

  if (loading) return <Loader />;

  return (
    <div className="container my-5">
      <h4 className="mb-3">All Products</h4>
      <div className="row">
        {products.map((p) => (
          <div className="col-md-3 mb-3" key={p.id}>
            <ProductCard product={p} onAddToCart={() => {}} />
          </div>
        ))}
      </div>
    </div>
  );
}
