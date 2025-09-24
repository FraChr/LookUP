<script setup>
defineProps({
  modelValue: [String, Number],
  options: {
    type: Array,
    required: true,
  },
  defaultOptionValue: [String],
  defaultOption: Boolean,
});

const emit = defineEmits(['update:modelValue']);
function onChange(event) {
  emit('update:modelValue', event.target.value);
}
</script>
<template>
  <select :value="modelValue" @change="onChange" class="border-2 px-2 py-1">
    <option v-if="defaultOption === true" value="" disabled>{{ defaultOptionValue }}</option>
    <option v-for="option in options" :key="option.id" :value="option.id">
      {{ option.name }}
    </option>
  </select>
</template>

<style scoped></style>
