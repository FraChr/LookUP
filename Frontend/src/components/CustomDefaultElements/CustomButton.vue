<script setup>
  defineProps({
    style: ''
  })
</script>

<template>
  <Button :class="style || 'cursor-pointer border-2 p-2 rounded-2xl hover:bg-amber-300'">
    <slot></slot>
  </Button>
</template>

<style scoped>

</style>