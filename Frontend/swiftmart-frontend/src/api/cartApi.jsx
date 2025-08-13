import axiosInstance from "./axiosInstance";

export const fetchCart = async (userId) => {
  const res = await axiosInstance.get(`/cart/${userId}`);
  return res.data;
};

export const addToCart = async (productId, quantity = 1) => {
  const res = await axiosInstance.post("/cart", { productId, quantity });
  return res.data;
};

export const updateCartItem = async (id, quantity) => {
  const res = await axiosInstance.put(`/cart/${id}`, { quantity });
  return res.data;
};

export const removeCartItem = async (id) => {
  const res = await axiosInstance.delete(`/cart/${id}`);
  return res.data;
};
// Pass userId if backend requires it
