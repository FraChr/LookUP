<script setup>
  import CustomButton from '@/components/CustomDefaultElements/CustomButton.vue';
  import { onMounted, watch } from 'vue';
  const props = defineProps({
    modelValue: {
      type: [Object, Array],
      required: true,
    },
    actions: {
      type: Array,
      default: () => ['edit', 'confirm'],
    },
    labels: {},
    styles: ''
  });
  const emit = defineEmits(['update:modelValue', 'confirm', 'edit']);

  const updateModelValue = (key, bool) => {
    return {
      ...props.modelValue,
      [key]: bool
    };
  }

  function onEdit(key) {
    const updated = updateModelValue(key, true);
    emit('update:modelValue', updated);
    emit('edit', key);
  }

  function onConfirm(key) {
    emit('confirm', key);
    emit('update:modelValue', updateModelValue(key, false));
  }

  function onCancel(key) {
    const updated = updateModelValue(key, false);
    emit('update:modelValue', updated);
  }

</script>

<template>

  <div v-for="(value, key) in modelValue" :key="key" class="w-full flex gap-2 flex-col">
    <h3 class="font-bold flex justify-center">{{ labels[key] ?? key }}</h3>
    <div class="flex flex-row" v-if="value === true">
      <CustomButton class="w-full" @click="onConfirm(key)">Acc</CustomButton>
      <CustomButton class="w-full" @click="onCancel(key)">X</CustomButton>
    </div>

    <template v-else>
      <CustomButton v-if="actions.includes('edit')" @click="onEdit(key)">Edit</CustomButton>
    </template>

    <slot name="customActions" :keyName="key" :editing="value === true"></slot>
  </div>
</template>

<style scoped>

</style>