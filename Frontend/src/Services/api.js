import axios from 'axios';
// baseURL: 'http://localhost:5223',
const apiClient = axios.create({
  baseURL: 'http://localhost:5000',
  headers: {
    'Content-Type': 'application/json',
  },
});

apiClient.interceptors.request.use((config) => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
    console.log(`token applied : ${token}`);
  }else {
    console.log('token not found');
  }

  return config;
});

export const getStorage = (params = {}) => apiClient.get('/storage', params);
export const getItemById = (id) => apiClient.get(`/storage/${id}`);
export const deleteItem = (id) => apiClient.delete(`/storage/${id}`);
export const addItem = (item) => apiClient.post(`/storage`, item);
export const addUser = (item) => apiClient.post(`/user`, item);
export const updateItem = (id, item) => apiClient.put(`/storage/${id}`, item);

export const getRooms = () => apiClient.get('/location');

export const auth = (credentials) => apiClient.post('/auth/login', credentials);
