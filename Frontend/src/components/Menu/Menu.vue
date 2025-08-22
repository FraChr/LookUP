<script setup>
  import {ref, watch} from 'vue';
  import MenuItems from '@/components/Menu/MenuItems.vue'
  import {useRoute} from 'vue-router';

  const isMenuOpen = ref(false);
  const route = useRoute();

  watch(route, () => {
    isMenuOpen.value = false;
  });

  const toggleMenu = () => {
    isMenuOpen.value = !isMenuOpen.value;
  }
</script>

<template>
  <div>
    <button @click="toggleMenu" class="cursor-pointer relative w-8 h-8 flex flex-col justify-around items-center">
      <span class="w-6 h-0.5 bg-black"></span>
      <span class="w-6 h-0.5 bg-black"></span>
      <span class="w-6 h-0.5 bg-black"></span>
    </button>

   <MenuItems v-if="isMenuOpen"/>
  </div>
</template>

<style scoped>
</style>