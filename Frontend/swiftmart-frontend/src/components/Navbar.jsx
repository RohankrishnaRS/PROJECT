import React, { useContext } from "react";
import { Link } from "react-router-dom";
import { AuthContext } from "../contexts/AuthContext";

export default function Navbar() {
  const { user, logout } = useContext(AuthContext);

  return (
    <nav className="navbar navbar-expand-lg navbar-light bg-white shadow-sm sticky-top">
      <div className="container">
        <Link className="navbar-brand fw-bold text-primary" to="/">
          <img src="/logo.png" alt="SwiftMart" width="40" className="me-2" />
          SwiftMart
        </Link>

        {/* Search Bar */}
        <form className="d-flex mx-auto w-50">
          <input
            className="form-control me-2"
            type="search"
            placeholder="Search for products, brands and more"
          />
          <button className="btn btn-outline-primary" type="submit">
            Search
          </button>
        </form>

        <div className="d-flex align-items-center">
          {user ? (
            <>
              <span className="me-3">Hi, {user.fullName}</span>
              <button className="btn btn-sm btn-danger me-2" onClick={logout}>
                Logout
              </button>
            </>
          ) : (
            <Link className="btn btn-sm btn-primary me-2" to="/login">
              Login
            </Link>
          )}
          <Link className="btn btn-sm btn-outline-dark" to="/cart">
            <i className="bi bi-cart-fill"></i> Cart
          </Link>
        </div>
      </div>
    </nav>
  );
}
