import React from "react";

export default function Footer() {
  return (
    <footer className="bg-dark text-white mt-5">
      <div className="container py-4 d-flex justify-content-between">
        <div>
          <h5>SwiftMart</h5>
          <p className="mb-0">&copy; {new Date().getFullYear()} All rights reserved.</p>
        </div>
        <div>
          <a href="/" className="text-white me-3">About</a>
          <a href="/" className="text-white me-3">Contact</a>
          <a href="/" className="text-white">Privacy</a>
        </div>
      </div>
    </footer>
  );
}
