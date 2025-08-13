import axiosInstance from "./axiosInstance";

export const loginUser = async (credentials) => {
  const res = await axiosInstance.post("/auth/login", credentials);
  return res.data;
};

export const registerUser = async (data) => {
  const res = await axiosInstance.post("/auth/register", data);
  return res.data;
};

export const getProfile = async () => {
  const res = await axiosInstance.get("/auth/profile");
  return res.data;
};
