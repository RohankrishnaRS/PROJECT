import React, { useRef } from "react";
import HeroCarousel from "../components/HeroCarousel";
import CategoryCard from "../components/CategoryCard";
import ProductCard from "../components/ProductCard";

export default function Home() {
  const homeRef = useRef(null);

  const scrollToHome = () => {
    homeRef.current.scrollIntoView({ behavior: "smooth" });
  };

  const categories = [
    { title: "Fashion", image: "/banners/banner1.jpg" },
    { title: "Electronics", image: "/banners/banner2.jpg" },
    { title: "Groceries", image: "/banners/banner3.jpg" },
  ];

  const trending = [
    { id: 1, name: "Smartphone", category: "Electronics", price: 19999 },
    { id: 2, name: "T-Shirt", category: "Fashion", price: 799 },
    { id: 3, name: "Headphones", category: "Electronics", price: 2999 },
  ];

  return (
    <div>
      {/* ✅ Hero Section */}
      <div
        className="hero-section d-flex flex-column justify-content-center align-items-center text-center text-white"
        style={{
          height: "100vh",
          background: "linear-gradient(rgba(0,0,0,0.5), rgba(0,0,0,0.5)), url('/banners/hero-bg.jpg') center/cover no-repeat",
        }}
      >
        <h1 className="fw-bold display-3">Welcome to SwiftMart</h1>
        <p className="lead mb-4">Shop the latest fashion, gadgets & groceries in one place</p>
        <button
          className="btn btn-lg btn-primary shadow pulse-btn"
          onClick={scrollToHome}
        >
          Get Started
        </button>
      </div>

      {/* ✅ Main Home Section */}
      <div ref={homeRef}>
        <HeroCarousel />

        {/* Categories */}
        <div className="container my-5">
          <h4 className="mb-3 fw-bold">Shop by Category</h4>
          <div className="row">
            {categories.map((cat, i) => (
              <div key={i} className="col-md-4 mb-3">
                <CategoryCard title={cat.title} image={cat.image} />
              </div>
            ))}
          </div>
        </div>

        {/* Trending */}
        <div className="container my-5">
          <h4 className="mb-3 fw-bold">🔥 Trending Products</h4>
          <div className="row">
            {trending.map((p) => (
              <div key={p.id} className="col-md-3 mb-3">
                <ProductCard product={p} onAddToCart={() => {}} />
              </div>
            ))}
          </div>
        </div>
      </div>
    </div>
  );
}
