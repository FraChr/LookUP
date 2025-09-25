<script setup>
import { ref } from 'vue';
import CustomButton from '@/components/CustomDefaultElements/CustomButton.vue';
import CustomInput from '@/components/CustomDefaultElements/CustomInput.vue';

const searchTerm = ref('');

const emit = defineEmits(['search']);

const handleSearch = () => {
  emit('search', searchTerm.value);
};
</script>

<template>
<!--  <div class="mb-5 flex justify-center gap-5">-->
<!--    <CustomButton >X</CustomButton>-->
<!--    <CustomInput v-model="searchTerm" />-->
<!--    <CustomButton @click="handleSearch">Search</CustomButton>-->
<!--  </div>-->

  <div class="flex justify-center">
    <div class="mb-5 max-w-md flex flex-row border p-1">
      <CustomInput :style="'flex-grow p-2  border-none focus:outline-none'" v-model="searchTerm" />
      <CustomButton :style="''">X</CustomButton>
      <CustomButton @click="handleSearch">Search</CustomButton>
    </div>
  </div>
</template>

<style scoped></style>
