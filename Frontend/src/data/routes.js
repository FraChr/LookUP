import HomePageView from '@/View/HomePageView.vue';
import StorageView from '@/View/StorageView.vue';
import ProfileView from '@/View/ProfileView.vue';
import ItemPageView from '@/View/ItemPageView.vue';
import LoginView from '@/View/LoginView.vue';
import SignupView from '@/View/signupView.vue';

export const routes = [
  { path: '/', component: HomePageView },
  {
    path: '/storage',
    component: StorageView,
    meta: { requiresAuth: true },
  },
  {
    path: '/user',
    component: ProfileView,
    meta: { requiresAuth: true },
  },
  {
    path: '/storage/:id',
    component: ItemPageView,
    meta: { requiresAuth: true },
  },
  {
    path: '/login',
    component: LoginView,
    meta: { guestOnly: true },
  },
  {
    path: '/signup',
    component: SignupView,
    meta: { guestOnly: true },
  },
];
