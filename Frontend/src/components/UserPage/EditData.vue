<script setup>
  import CustomInput from '@/components/CustomDefaultElements/CustomInput.vue';
  import CustomButton from '@/components/CustomDefaultElements/CustomButton.vue';

  const props = defineProps({
    modelValue: {},
    keyName: Number,
    value: String,
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

<style scoped>

</style>