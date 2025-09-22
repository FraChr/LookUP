<script setup>
import CustomInput from '@/components/CustomDefaultElements/CustomInput.vue';

const props = defineProps({
  modelValue: [String, Number],
  number: Boolean
});

const emit = defineEmits(['update:modelValue']);

function updateValue(val) {
  emit('update:modelValue', val);
}
</script>

<template>
    <CustomInput :modelValue="modelValue"
    @update:modelValue="updateValue"/>
</template>

<style scoped></style>