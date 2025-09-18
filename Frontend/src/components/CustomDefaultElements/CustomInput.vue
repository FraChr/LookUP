<script setup>
  defineProps({
    modelValue: [String, Number],
    style: '',
  });

  const emit = defineEmits(['update:modelValue']);
 function onInput(event) {
   emit('update:modelValue', event.target.value);
 }
</script>

<template>
  <input :class="style || 'border border-gray-300 px-2 py-1 rounded'" :value="modelValue" @input="onInput"/>
</template>

<style scoped>

</style>