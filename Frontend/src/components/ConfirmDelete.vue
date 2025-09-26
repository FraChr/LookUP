<script setup>
import CustomButton from '@/components/CustomDefaultElements/CustomButton.vue';
const props = defineProps({
  message: '',
  showConfirmDelete: {
    type: Boolean,
    required: true,
  },
});

const emit = defineEmits(['confirmDelete', 'cancelDelete']);

</script>

<template>
<!--  <div class="flex flex-col" v-if="showConfirmDelete">-->
    <div class="flex flex-col gap-4" v-if="showConfirmDelete">
      <div>
        <p class="text-red-600 text-xl">{{ message }}</p>
      </div>
      <div class="flex flex-row gap-4 justify-center">
        <CustomButton @click="emit('confirmDelete')">Yes, Delete</CustomButton>
        <CustomButton @click="emit('cancelDelete')">Cancel</CustomButton>
      </div>
    </div>
<!--  </div>-->
</template>

<style scoped></style>