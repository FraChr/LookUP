<script setup>

</script>

<template>
  <div class="grid grid-cols-2 gap-4 w-full">
    <div>
      <slot name="Right"></slot>
    </div>

    <div class="flex flex-col gap-4">
      <slot name="Left"></slot>
    </div>
  </div>
</template>

<style scoped>

</style>