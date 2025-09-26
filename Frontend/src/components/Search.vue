<script setup>
import { ref } from 'vue';
import CustomButton from '@/components/CustomDefaultElements/CustomButton.vue';
import CustomInput from '@/components/CustomDefaultElements/CustomInput.vue';

const searchTerm = ref('');

const emit = defineEmits(['search', 'clearSearch']);

const handleSearch = () => {
  emit('search', searchTerm.value);
};

const clearSearch = () => {
  emit('clearSearch', searchTerm.value = null);
}

</script>

<template>
  <div class="flex justify-center">
    <div class="mb-5 max-w-md flex flex-row border-2 border-contrast p-1">
      <CustomButton @click="clearSearch">X</CustomButton>
      <CustomInput :style="'flex-grow p-2  border-none focus:outline-none'" v-model="searchTerm" />
      <CustomButton @click="handleSearch">Search</CustomButton>
    </div>
  </div>
</template>

<style scoped></style>
