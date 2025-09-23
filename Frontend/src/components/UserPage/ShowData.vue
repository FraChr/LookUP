<script setup>
  const props = defineProps({
    value: ''
  });
</script>

<template>
  <div>
    <slot>{{ value }}</slot>
  </div>
</template>

<style scoped>

</style>