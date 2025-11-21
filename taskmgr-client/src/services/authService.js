const REACT_APP_API_URL = process.env.REACT_APP_API_URL;

// Зберігання токена
const TOKEN_KEY = 'auth_token';

const setToken = (token) => {
  localStorage.setItem(TOKEN_KEY, token);
};

const getToken = () => {
  return localStorage.getItem(TOKEN_KEY);
};

const removeToken = () => {
  localStorage.removeItem(TOKEN_KEY);
};

// Реєстрація
export const register = async (username, password, email) => {
  const response = await fetch(`${REACT_APP_API_URL}/Auth/Register`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/jsson',
    },
    body: JSON.stringify({ username, password, email }),
  });
  if (!response.ok) {
    throw new Error('Register failed');
  }
  const data = await response.body;
  if (data) setToken(data);
  return data;
};

// Логін
export const login = async (username, password) => {
  const response = await fetch(`${REACT_APP_API_URL}/Auth/Login`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({ username, password }),
  });
  if (!response.ok) {
    throw new Error('Login failed');
  }
  const data = await response.body;
  if (data) setToken(data);
  return data;
};

// Отримати інформацію про користувача
export const getUserInfo = async () => {
  const token = getToken();
  if (!token) throw new Error('No token');
  const response = await fetch(`${REACT_APP_API_URL}/Auth/GetUserInfo`, {
    method: 'GET',
    headers: {
      'Authorization': `Bearer ${token}`,
    },
  });
  if (!response.ok) {
    throw new Error('Failed to get user info');
  }
  return await response.json();
};

// Логаут
export const logout = () => {
  removeToken();
};

// Додатково: отримати токен для інших сервісів
export const getAuthToken = getToken;