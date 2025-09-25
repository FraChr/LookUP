<script setup>
  const props = defineProps({
    value: '',
  });
</script>

<template>
  <div class="flex justify-center p-5 m-2 border rounded-2xl bg-olive">
    <span>{{value}}</span>
  </div>
</template>

<style scoped>

</style>