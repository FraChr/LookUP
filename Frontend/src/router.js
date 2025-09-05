import { createRouter, createWebHistory } from 'vue-router';

import HomePageView from './View/HomePageView.vue';
import StorageView from '@/View/StorageView.vue';
import ItemPageView from '@/View/ItemPageView.vue';
import ProfileView from '@/View/ProfileView.vue';

const routes = [
  { path: '/', component: HomePageView },
  { path: '/storage', component: StorageView },
  { path: '/user', component: ProfileView },
  { path: '/storage/:id', component: ItemPageView },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
