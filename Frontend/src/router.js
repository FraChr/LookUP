import { createRouter, createWebHistory } from 'vue-router';

import HomePageView from './View/HomePageView.vue';
import StorageView from '@/View/StorageView.vue';
import ItemPageView from '@/View/ItemPageView.vue';
import ProfileView from '@/View/ProfileView.vue';
import LoginView from '@/View/LoginView.vue';
import SignupView from '@/View/signupView.vue';

const routes = [
  { path: '/', component: HomePageView },
  { path: '/storage', component: StorageView },
  { path: '/user', component: ProfileView },
  { path: '/storage/:id', component: ItemPageView },
  { path: '/login', component: LoginView },
  { path: '/signup', component: SignupView },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
