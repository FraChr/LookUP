<script setup>
  import { computed } from 'vue';

  const props = defineProps({
    style: '',
  });

  const defaultStyle = "cursor-pointer border-2 border-contrast p-2 rounded-2xl text-primary bg-extra-2 text-text hover:bg-contrast hover:text-primary";
  const finalClass = computed(() => props.style || defaultStyle);
</script>

<template>
  <Button :class="finalClass">
    <slot></slot>
  </Button>
</template>

<style scoped>

</style>