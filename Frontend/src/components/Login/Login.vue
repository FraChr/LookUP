<script setup>
  import { useAccessControl } from '@/composable/useAccsessControl.js';

  const { login, email, error, password } = useAccessControl();


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
  <div class="text-red-700">
    {{error}}
  </div>
</template>

<style scoped>

</style>