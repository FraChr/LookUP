<script setup>
  import { ref } from 'vue';

  const searchTerm = ref('');

  const emit = defineEmits(['search']);

  const handleSearch = () => {
    emit('search', searchTerm.value);
  };
</script>

<template>
  <div class="mb-5 flex justify-center gap-5">
    <input v-model="searchTerm" class="border-2" />
    <button @click="handleSearch" class="border-2 px-2 py-1 rounded-2xl">Search</button>
  </div>
</template>

<style scoped></style>
