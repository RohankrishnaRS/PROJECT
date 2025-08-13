import axiosInstance from "./axiosInstance";

export const fetchUsers = async () => {
  const res = await axiosInstance.get("/admin/users");
  return res.data;
};

export const fetchSellers = async () => {
  const res = await axiosInstance.get("/admin/sellers");
  return res.data;
};

export const fetchReports = async () => {
  const res = await axiosInstance.get("/admin/reports");
  return res.data;
};
