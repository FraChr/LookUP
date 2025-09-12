<script setup>
  import { useRouter } from 'vue-router';
  import { useAccessControl } from '@/composable/useAccsessControl.js';

  const { logout, hasToken } = useAccessControl();
  const router = useRouter();

</script>

<template>
  <div>
    <nav class="z-50 absolute h-full w-1/3 left-0 bg-gray-600">
      <ul class="p-5 pl-2 pr-2">
        <li class="mb-1"><RouterLink to="/">Home</RouterLink></li>
        <li class="mb-1"><RouterLink to="/storage">Storage</RouterLink></li>
        <li class="mb-1"><RouterLink to="/user">UserPage</RouterLink></li>
        <li v-if="!hasToken">
          <button @click="router.push('/signup')" class="hover:bg-white bg-amber-200 border-2 px-6 py-2">
            Signup
          </button>
        </li>
        <li v-if="!hasToken">
          <button @click="router.push('/login')" class="hover:bg-white bg-amber-200 border-2 px-6 py-2">
            Log in
          </button>
        </li>
        <li v-if="hasToken">
          <button @click="logout" class="hover:bg-white bg-amber-200 border-2 px-6 py-2">
            Log Out
          </button>
        </li>
      </ul>
    </nav>
  </div>
</template>

<style scoped></style>
