<script setup lang="ts">
  import {ref} from 'vue';

  const isMenuOpen = ref(false);

  const toggleMenu = () => {
    isMenuOpen.value = !isMenuOpen.value;
  }
</script>

<template>

  <button>help</button>
</template>

<style scoped>

</style>