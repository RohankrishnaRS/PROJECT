import axiosInstance from "./axiosInstance";

export const fetchReviews = async (productId) => {
  const res = await axiosInstance.get(`/review/${productId}`);
  return res.data;
};

export const addReview = async (data) => {
  const res = await axiosInstance.post("/review", data);
  return res.data;
};
