<script setup>
  import Menu from './Menu.vue'
</script>

<template>
  <div class="container">
    <h1 class="text-black text-2xl">LookUp</h1>
    <img src="data:" alt width="50" height="50"/>
    <Menu/>
  </div>
</template>

<style scoped>
  .container {
    display: flex;
    flex-direction: row;
    justify-content: center;
    align-items: center;
  }
</style>