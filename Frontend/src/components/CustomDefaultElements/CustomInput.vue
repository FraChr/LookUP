<script setup>
  import { computed } from 'vue';

  const props = defineProps({
    modelValue: [String, Number],
    style: '',
  });

  const defaultStyle = 'border-2 rounded border-contrast bg-primary text-text px-2 py-1 focus:outline-primary';
  const emit = defineEmits(['update:modelValue']);

  const finalStyle = computed(() => props.style || defaultStyle);
 function onInput(event) {
   emit('update:modelValue', event.target.value);
 }
</script>

<template>
  <input :class="finalStyle" :value="modelValue" @input="onInput"/>
</template>

<style scoped>

</style>