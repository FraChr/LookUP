import { auth } from '@/Services/api.js';
import { useRouter } from 'vue-router';
import { computed, ref } from 'vue';

export function useAccessControl() {

  const router = useRouter();
  const email = ref('');
  const password = ref('');
  let error = ref('');

  const token = computed(() => getToken());

  const hasToken = computed(() => !!token.value);

  async function login() {
    error.value = '';
    try {
      const response = await auth({
        identifier: email.value,
        password: password.value,
      });

      const token = response.data.token;

      console.log("login data", response.data);

      localStorage.setItem('token', token);
      await router.push('/');
    } catch (e) {
      if(e.response.status === 401) {
        error.value = 'Invalid username or password';
        email.value = '';
        password.value = '';
      }
      console.log(e.response);
    }
  }

  const logout = () => {
    localStorage.removeItem('token');
    window.location.href = '/';
  }

  const getToken = () => {
    return localStorage.getItem('token')
  }

  return{
    email,
    password,
    error,
    hasToken,
    login,
    logout,
    getToken,
  }
}