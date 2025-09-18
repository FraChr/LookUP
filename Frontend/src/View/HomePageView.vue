<script setup>
import AddItemForm from '@/components/AddItemForm.vue';
import { useAccessControl } from '@/composable/useAccsessControl.js';

const { hasToken } = useAccessControl();
</script>

<template>
  <div>
    <h1>{{ hasToken ? 'Welcome Username' : 'Welcome to homepage' }}</h1>
  </div>

  <div v-if="hasToken" class="w-full border-2 border-amber-600 flex justify-center">
    <AddItemForm />
  </div>
</template>

<style></style>
