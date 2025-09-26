<script setup>
  defineProps({
    visible: {
      type: Boolean,
      required: true,
    }
  });
</script>

<template>
  <teleport to="body">
    <div v-if="visible" class="fixed inset-0 bg-black/30 backdrop-blur-sm z-50 flex items-center justify-center">
      <div>
        <slot />
      </div>
    </div>
  </teleport>
</template>

<style scoped>

</style>