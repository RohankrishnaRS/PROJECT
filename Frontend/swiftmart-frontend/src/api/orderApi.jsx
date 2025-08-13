import axiosInstance from "./axiosInstance";

export const fetchOrders = async () => {
  const res = await axiosInstance.get("/order");
  return res.data;
};

export const createOrder = async (data) => {
  const res = await axiosInstance.post("/order", data);
  return res.data;
};
