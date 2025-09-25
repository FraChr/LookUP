import { createRouter, createWebHistory } from 'vue-router';
import { useAccessControl } from '@/composable/useAccsessControl.js';
import { routes } from '@/data/routes.js';

const router = createRouter({
  history: createWebHistory(),
  routes,
});
router.beforeEach((to, from, next) => {
  const { hasToken } = useAccessControl();
  const isAuthenticated = hasToken.value;

  if (to.meta.requiresAuth && !isAuthenticated) {
    next('/login');
  } else if (to.meta.guestOnly && isAuthenticated) {
    next('/');
  } else {
    next();
  }
});

export default router;
