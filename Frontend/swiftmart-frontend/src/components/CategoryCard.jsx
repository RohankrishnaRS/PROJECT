import React from "react";
import { motion } from "framer-motion";

export default function CategoryCard({ title, image }) {
  return (
    <motion.div
      whileHover={{ scale: 1.05 }}
      className="card shadow-sm border-0"
      style={{ cursor: "pointer" }}
    >
      <img src={image} className="card-img-top" alt={title} height="180" style={{ objectFit: "cover" }} />
      <div className="card-body text-center">
        <h6 className="fw-bold">{title}</h6>
      </div>
    </motion.div>
  );
}
