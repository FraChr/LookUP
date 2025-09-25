<script setup>
  const props = defineProps({
    value: '',
  });
</script>

<template>
  <div class="flex justify-center p-5 m-2 border-2 border-contrast rounded-2xl
      text-text text-xl bg-extra-2 hover:bg-contrast hover:text-primary
      focus:outline-primary">
    <span>{{value}}</span>
  </div>
</template>

<style scoped>

</style>