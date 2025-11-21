import axios from 'axios';

const API_URL = process.env.REACT_APP_API_URL;

export const login = async (username, password) => {
  const response = await axios.post(`${API_URL}/Auth/Login`, {
    username,
    password,
  });
  // Відповідно до openapi.json, токен може бути неявно в response.data (без поля token)
  // Тому збережемо весь response.data як токен, якщо це рядок
  if (typeof response.data === "string") {
    localStorage.setItem('auth_token', response.data);
  } else if (response.data && response.data.token) {
    localStorage.setItem('auth_token', response.data.token);
  }
  return response.data;
};

export const register = async (username, password, email) => {
  const response = await axios.post(`${API_URL}/Auth/Register`, {
    username,
    password,
    email,
  });
  // Аналогічно для реєстрації
  if (typeof response.data === "string") {
    localStorage.setItem('auth_token', response.data);
  } else if (response.data && response.data.token) {
    localStorage.setItem('auth_token', response.data.token);
  }
  return response.data;
};

export const getUserInfo = async () => {
  const token = localStorage.getItem('auth_token');
  const response = await axios.get(`${API_URL}/Auth/GetUserInfo`, {
    headers: token ? { Authorization: `Bearer ${token}` } : {}
  });
  return response.data;
};