import axiosInstance from "./axiosInstance";

export const processPayment = async (data) => {
  const res = await axiosInstance.post("/payment", data);
  return res.data;
};

export const fetchPayments = async () => {
  const res = await axiosInstance.get("/payment");
  return res.data;
};
