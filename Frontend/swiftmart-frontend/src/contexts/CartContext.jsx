import React, { createContext, useState, useEffect } from "react";
import { fetchCart, addToCart, updateCartItem, removeCartItem } from "../api/cartApi";
import useAuth from "../hooks/useAuth";

export const CartContext = createContext();

export const CartProvider = ({ children }) => {
  const { user } = useAuth();   // ✅ bring user from context
  const [cart, setCart] = useState([]);
  const [loading, setLoading] = useState(true);

  // ✅ Load cart only if user is logged in
  useEffect(() => {
    if (user && user.id) {
      setLoading(true);
      fetchCart(user.id)
        .then((data) => setCart(data))
        .catch((err) => console.error("Cart fetch failed", err))
        .finally(() => setLoading(false));
    } else {
      setCart([]);   // clear cart if no user
      setLoading(false);
    }
  }, [user]);

  const addItem = async (productId, quantity = 1) => {
    const data = await addToCart(productId, quantity);
    setCart(data);
  };

  const updateItem = async (id, quantity) => {
    const data = await updateCartItem(id, quantity);
    setCart(data);
  };

  const removeItem = async (id) => {
    const data = await removeCartItem(id);
    setCart(data);
  };

  return (
    <CartContext.Provider value={{ cart, addItem, updateItem, removeItem, loading }}>
      {children}
    </CartContext.Provider>
  );
};
