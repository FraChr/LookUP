import { createRouter, createWebHistory } from 'vue-router';

import HomePage from './components/HomePage/HomePage.vue';
import StoragePage from './components/StoragePage/StorageView.vue';
import UserPage from './components/UserPage/UserView.vue';
import ItemPage from './components/StoragePage/ItemPage.vue';

const routes = [
  { path: '/', component: HomePage },
  { path: '/storage', component: StoragePage },
  { path: '/user', component: UserPage },
  { path: '/storage/:id', component: ItemPage },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
