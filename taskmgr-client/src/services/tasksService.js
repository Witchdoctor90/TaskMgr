import axios from 'axios';

const API_URL = process.env.REACT_APP_API_URL;

// Отримати токен напряму з localStorage
function getAuthToken() {
  return localStorage.getItem('auth_token');
}

// Додає Authorization header, якщо токен є
function authHeaders() {
  const token = getAuthToken();
  return token ? { Authorization: `Bearer ${token}` } : {};
}

// Отримати всі таски
export const getAllTasks = async () => {
  const response = await axios.get(`${API_URL}/Tasks/GetAll`, {
    headers: { ...authHeaders() }
  });
  return response.data;
};

// Отримати таску по id
export const getTaskById = async (id) => {
  const response = await axios.get(`${API_URL}/Tasks/GetById`, {
    params: { id },
    headers: { ...authHeaders() }
  });
  return response.data;
};

// Створити таску
export const createTask = async (task) => {
  const response = await axios.post(`${API_URL}/Tasks/Create`, task, {
    headers: { ...authHeaders() }
  });
  return response.data;
};

// Оновити таску
export const updateTask = async (task) => {
  const response = await axios.put(`${API_URL}/Tasks/Update`, task, {
    headers: { ...authHeaders() }
  });
  return response.data;
};

// Видалити таску
export const deleteTask = async (id) => {
  const response = await axios.delete(`${API_URL}/Tasks/Delete`, {
    data: id,
    headers: { ...authHeaders() }
  });
  return response.data;
};
