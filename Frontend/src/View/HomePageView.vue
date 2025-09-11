<script setup>
import AddItemForm from '@/components/AddItemForm.vue';
import { useRouter } from 'vue-router';
import { computed } from 'vue';

const router = useRouter();
const hasToken = computed(() => {
  const token = localStorage.getItem('token');
  if (token) {
    return true;
  }
  return false;
});
</script>

<template>
  <div>
    <h1>{{ hasToken ? 'Welcome Username' : 'Welcome to homepage' }}</h1>
    <button @click="router.push('/signup')" class="hover:bg-white bg-amber-200 border-2 px-6 py-2">
      Signup
    </button>
    <button @click="router.push('/login')" class="hover:bg-white bg-amber-200 border-2 px-6 py-2">
      Log in
    </button>
  </div>

  <div class="w-full border-2 border-amber-600 flex justify-center">
    <AddItemForm />
  </div>
</template>

<style></style>
