import axios from 'axios';
import { useJwtClaims } from '@/composable/useJwtClaims.js';
import {getToken, logout} from './tokenHandler.js'

const { tokenExpiry } = useJwtClaims();


const apiClient = axios.create({
  baseURL: 'http://localhost:5000',
  headers: {
    'Content-Type': 'application/json',
  },
});

apiClient.interceptors.request.use((config) => {
  const token = getToken();

  if(!token) {
    return config;
  }

  const expiry = tokenExpiry(token);
  const now = Date.now();

  if(expiry && now >= expiry) {
    logout();
    throw Error('Token expired.');
  }
  config.headers.Authorization = `Bearer ${token}`;

  return config;
});


export const createCrudService = (route) => ({
  list: (params = {}) => apiClient.get(`/${route}`, params),
  getById: (id) => apiClient.get(`/${route}/${id}`),
  create: (data) => apiClient.post(`/${route}`, data),
  update: (id, data) => apiClient.put(`/${route}/${id}`, data),
  delete: (id) => apiClient.delete(`/${route}/${id}`)
});


export const storageService = createCrudService('storage');


// export const getStorage = (params = {}) => apiClient.get('/storage', params);
// export const getItemById = (id) => apiClient.get(`/storage/${id}`);
// export const deleteItem = (id) => apiClient.delete(`/storage/${id}`);

// export const updateItem = (id, item) => apiClient.put(`/storage/${id}`, item);


// export const getRooms = () => apiClient.get('/location');

export const addUser = (user) => apiClient.post(`/signup`, user);
export const auth = (credentials) => apiClient.post('/auth/login', credentials);
