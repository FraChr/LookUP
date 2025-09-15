import { auth } from '@/Services/api.js';
import { useRouter } from 'vue-router';
import { computed, ref } from 'vue';

import {setLogoutHandler, setTokenHandler} from '@/Services/tokenHandler.js';

export function useAccessControl() {

  const router = useRouter();
  const email = ref('');
  const password = ref('');
  let error = ref('');

  const token = ref(getToken());

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
    }
  }

  async function logout(){
    localStorage.removeItem('token');
    // await router.push('/');
    window.location.pathname = '/';
  }

  function getToken() {
    return localStorage.getItem('token')
  }



  setTokenHandler(getToken);
  setLogoutHandler(logout);

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