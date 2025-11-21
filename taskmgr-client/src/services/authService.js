import axios from 'axios';

const API_URL = process.env.REACT_APP_API_URL;

export const login = async (username, password) => {
  const response = await axios.post(`${API_URL}/Auth/Login`, {
    username,
    password,
  });
  return response.data; // Повертаємо дані з відповіді
};

export const register = async (username, password, email) => {
  const response = await axios.post(`${API_URL}/Auth/Register`, {
    username,
    password,
    email,
  });
  return response.data; // Повертаємо дані з відповіді
};

export const getUserInfo = async () => {
  const token = localStorage.getItem('auth_token');
  const response = await axios.get(`${API_URL}/Auth/GetUserInfo`, {
    headers: token ? { Authorization: `Bearer ${token}` } : {}
  });
  return response.data; // Повертаємо дані з відповіді
};