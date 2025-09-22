<script setup>
  const props = defineProps({
    value: ''
  });
</script>

<template>
<!--  <p>{{ value }}</p>-->
  <slot>{{ value }}</slot>
</template>

<style scoped>

</style>