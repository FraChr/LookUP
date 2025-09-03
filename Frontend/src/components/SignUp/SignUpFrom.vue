<script setup>
  import { ref } from 'vue';
  import { addUser} from '@/Services/api.js';

  const userName = ref('');
  const password = ref('');
  const email = ref('');


  const register = async () => {
    try {
      const user = {
        userName: userName.value,
        password: password.value,
        email: email.value,
      }
      await addUser(user);

      userName.value = '';
      password.value = '';
      email.value = '';
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
      <input v-model="userName" type="text" placeholder="Enter Username" class="border-2 p-2"/>
      <input v-model="password" type="password" placeholder="Enter Password" class="border-2 p-2"/>
      <input v-model="email" type="email" placeholder="Enter Email" class="border-2 p-2"/>
      <button type="submit" class="hover:bg-white bg-amber-200 border-2 px-6 py-2">Submit</button>
    </form>
  </div>
</template>

<style scoped>

</style>