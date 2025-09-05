<script setup>
  import { ref } from 'vue';
  import { addUser} from '@/Services/api.js';

  const userInfo = ref({
    userName: '',
    password: '',
    passwordConfirmation: '',
    email: '',
  });

  const register = async () => {
    try {
      const user = {
        userName: userInfo.value.userName,
        password: userInfo.value.password,
        passwordConfirmation: userInfo.value.passwordConfirmation,
        email: userInfo.value.email,
      }

      console.log("user to be sent to api: ", user);

      await addUser(user);

      userInfo.value.userName = '';
      userInfo.value.password = '';
      userInfo.value.passwordConfirmation = '';
      userInfo.value.email = '';
    } catch (error) {
      if(error.response && error.response.data.message) {
        console.error('error response: ', error.response.data.message);
      } else {
        console.error("unknown error: ", error.message);
      }

    }
  }

</script>

<template>
  <div class="w-full flex flex-col items-center m-4">
    <form @submit.prevent="register" class="border-2 w-full max-w-md flex flex-col space-y-3 p-6">
      <input v-model="userInfo.userName" type="text" placeholder="Enter Username" class="border-2 p-2"/>
      <input v-model="userInfo.password" autocomplete="true" type="password" placeholder="Enter Password" class="border-2 p-2"/>
      <input v-model="userInfo.passwordConfirmation" autocomplete="true" type="password" placeholder="Enter Password" class="border-2 p-2"/>
      <input v-model="userInfo.email" type="email" placeholder="Enter Email" class="border-2 p-2"/>
      <button type="submit" class="hover:bg-white bg-amber-200 border-2 px-6 py-2">Sign Up</button>
    </form>
  </div>
</template>

<style scoped>

</style>