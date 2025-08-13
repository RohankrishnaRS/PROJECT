import axiosInstance from "./axiosInstance";

export const fetchSellerProducts = async () => {
  const res = await axiosInstance.get("/seller/products");
  return res.data;
};

export const fetchSellerOrders = async () => {
  const res = await axiosInstance.get("/seller/orders");
  return res.data;
};
