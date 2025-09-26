<script setup>
  import CustomButton from '@/components/CustomDefaultElements/CustomButton.vue';

  const props = defineProps({
    currentPage: 0,
    totalPages: 0,
  });
  const emit = defineEmits(['nextPage', 'previousPage']);

  const nextPage = () => {
    if (props.currentPage >= props.totalPages) return;
    emit('nextPage');
  };

  const prevPage = () => {
    if (props.currentPage <= 1) return;
    emit('previousPage');
  };

</script>

<template>
  <div class="flex flex-col items-center mt-4 select-none">
    <div class="flex justify-around w-full mb-2">
      <CustomButton
        @click="prevPage"
        :disabled="currentPage === 1"
        class="disabled:opacity-50 disabled:line-through"
      >
        Previous
      </CustomButton>
      <CustomButton
        @click="nextPage"
        :disabled="currentPage === totalPages"
        class="disabled:opacity-50 disabled:line-through"
      >
        Next
      </CustomButton>
    </div>
    <span class="text-text"
    >Page {{ currentPage }} of {{ totalPages }}</span
    >
  </div>
</template>

<style scoped>

</style>