import React from "react";
import useCart from "../hooks/useCart";
import CartItem from "../components/CartItem";
import Loader from "../components/Loader";

export default function Cart() {
  const { cart, removeItem, updateItem, loading } = useCart();

  if (loading) return <Loader />;

  return (
    <div className="container my-5">
      <h4 className="mb-3">My Cart</h4>
      {cart.length === 0 ? (
        <p>Your cart is empty.</p>
      ) : (
        cart.map((item) => (
          <CartItem
            key={item.id}
            item={item}
            onRemove={removeItem}
            onQuantityChange={updateItem}
          />
        ))
      )}
      {cart.length > 0 && (
        <button className="btn btn-success mt-3">Proceed to Checkout</button>
      )}
    </div>
  );
}
