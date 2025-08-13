import React from "react";
import { Carousel } from "react-bootstrap";
import { motion } from "framer-motion";

export default function HeroCarousel() {
  return (
    <motion.div
      initial={{ opacity: 0, y: -50 }}
      animate={{ opacity: 1, y: 0 }}
      transition={{ duration: 0.8 }}
    >
      <Carousel fade>
        <Carousel.Item>
          <img className="d-block w-100" src="/banners/banner1.jpg" alt="Fashion" style={{ maxHeight: "500px", objectFit: "cover" }} />
          <Carousel.Caption className="bg-dark bg-opacity-50 rounded p-2">
            <h3>Fashion Mega Sale</h3>
            <p>Trendy clothes at unbeatable prices.</p>
          </Carousel.Caption>
        </Carousel.Item>
        <Carousel.Item>
          <img className="d-block w-100" src="/banners/banner2.jpg" alt="Electronics" style={{ maxHeight: "500px", objectFit: "cover" }} />
          <Carousel.Caption className="bg-dark bg-opacity-50 rounded p-2">
            <h3>Latest Electronics</h3>
            <p>Upgrade to the newest gadgets today.</p>
          </Carousel.Caption>
        </Carousel.Item>
        <Carousel.Item>
          <img className="d-block w-100" src="/banners/banner3.jpg" alt="Groceries" style={{ maxHeight: "500px", objectFit: "cover" }} />
          <Carousel.Caption className="bg-dark bg-opacity-50 rounded p-2">
            <h3>Fresh Groceries</h3>
            <p>Delivered daily to your doorstep.</p>
          </Carousel.Caption>
        </Carousel.Item>
      </Carousel>
    </motion.div>
  );
}
