<script setup>
  import Menu from '../Menu/Menu.vue';
</script>

<template>
  <div class="flex flex-col">
    <header class="bg-contrast flex justify-between items-center px-5 py-3">
      <Menu />
      <h1 class="text-primary text-3xl font-bold">
        <RouterLink to="/"> LookUp </RouterLink>
      </h1>
      <RouterLink to="/user">
        <img src="../../assets/img/75.jpg" alt width="50" height="50" />
      </RouterLink>
    </header>
  </div>
</template>

<style scoped></style>
