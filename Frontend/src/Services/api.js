import axios from 'axios';

const apiClient = axios.create({
  baseURL: 'http://localhost:5223',
  headers: {
    'Content-Type': 'application/json',
  }
});

export const getStorage = (config = {}) => apiClient.get('/storage', config);
export const getItemById = (id) => apiClient.get(`/storage/${id}`);
export const deleteItem = (id) => apiClient.delete(`/storage/${id}`);
export const addItem = (item) => apiClient.post(`/storage`, item);
export const updateItem = (id, item) => apiClient.put(`/storage/${id}`, item);



export const getRooms = () => apiClient.get('/location');