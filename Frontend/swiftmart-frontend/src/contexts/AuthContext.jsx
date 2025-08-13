import React, { createContext, useState, useEffect } from "react";
import { loginUser, registerUser, getProfile } from "../api/authApi";
import { saveToken, getToken, removeToken } from "../utils/storage";

export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);
  const [loading, setLoading] = useState(true);

  // ✅ Load user from JWT on app start
  useEffect(() => {
    const token = getToken();
    if (token) {
      getProfile()
        .then((data) => setUser(data))
        .catch(() => logout());
    }
    setLoading(false);
  }, []);

  const login = async (credentials) => {
    const data = await loginUser(credentials);
    if (data.token) {
      saveToken(data.token);
      setUser(data.user);
    }
    return data;
  };

  const register = async (formData) => {
    const data = await registerUser(formData);
    if (data.token) {
      saveToken(data.token);
      setUser(data.user);
    }
    return data;
  };

  const logout = () => {
    removeToken();
    setUser(null);
  };

  return (
    <AuthContext.Provider value={{ user, login, register, logout, loading }}>
      {children}
    </AuthContext.Provider>
  );
};
