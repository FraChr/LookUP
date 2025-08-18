<script setup>
  import Menu from './Menu/Menu.vue';

</script>

<template>
  <header class="m-5 bg-sky-400 flex flex-row justify-between items-center pl-5 pr-5 pt-2 pb-2">
    <Menu/>
    <h1 class="text-black text-2xl">
      <RouterLink to="/">
        LookUp
      </RouterLink>
    </h1>
    <RouterLink to="/user">
      <img src="../assets/img/75.jpg" alt width="50" height="50"/>
    </RouterLink>
  </header>
</template>

<style scoped>
</style>