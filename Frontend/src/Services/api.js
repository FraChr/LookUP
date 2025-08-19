import axios from 'axios';

const apiClient = axios.create({
  baseURL: 'http://localhost:5223',
  headers: {
    'Content-Type': 'application/json',
  }
});

export const getStorage = () => apiClient.get('/storage');
export const deleteItem = (id) => apiClient.delete(`/storage/${id}`);
export const addItem = (item) => apiClient.post(`/storage`, item);



export const getRooms = () => apiClient.get('/location');