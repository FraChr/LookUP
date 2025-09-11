<script setup>
  import {ref} from 'vue';
  import { auth } from '@/Services/api.js';
  import { useRouter } from 'vue-router';

  const router = useRouter();
  const email = ref('');
  const password = ref('');
  let error = ref('');

  async function login() {
    error.value = '';
    try {
      const response = await auth({
        identifier: email.value,
        password: password.value,
      });

      const token = response.data.token;
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
  }

</script>

<template>
  <div class="w-full flex flex-col items-center m-4">
    <form @submit.prevent="login" class="border-2 w-full max-w-md flex flex-col space-y-3 p-6">
      <input v-model="email" type="text" placeholder="Enter Email" class="border-2 p-2"/>
      <input v-model="password" autocomplete="true" type="password" placeholder="Enter Password" class="border-2 p-2"/>
      <button type="submit" class="hover:bg-white bg-amber-200 border-2 px-6 py-2">Log in</button>
      <span class="text-center"><RouterLink to="/signup">Register Here</RouterLink></span>
    </form>
  </div>

  <div>
    <span>{{error.value}}</span>

  </div>
  <div class="text-red-700">
    {{error}}
  </div>
</template>

<style scoped>

</style>