<script setup>
  import {ref} from 'vue';
  // import {auth} from '@/firebase.js';
  // import {signInWithEmailAndPassword} from "firebase/auth";
  import axios from 'axios';
  import { auth } from '@/Services/api.js';

  const email = ref('');
  const password = ref('');
  const error = ref('');

  async function login() {
    error.value = '';
    try {
      // const userCredential = await signInWithEmailAndPassword(auth, email.value, password.value);
      // const token = await userCredential.user.getIdToken();
      // console.log("Firebase JWT: ", token);
      console.log("at login");
      console.log(`${email.value}@${password.value}`);
      const response = await auth({
        identifier: email.value,
        password: password.value,
      });

      const token = response.data.token;
      localStorage.setItem('token', token);
      console.log(token);
    } catch (e) {
      if(e.response && e.response.data){
        error.value = e.response.data.title || e.response.data.message || 'Login failed.';
      } else {
        error.value = 'Login failed.';
      }
      // error.value = e.message;
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
    </form>
  </div>
  <div>
    <span>{{error.value}}</span>
    <button @click="logout">Log Out</button>
  </div>
</template>

<style scoped>

</style>