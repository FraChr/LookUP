<script setup>
  import CustomButton from '@/components/CustomDefaultElements/CustomButton.vue';

  const props = defineProps({
    currentPage: 0,
    totalPages: 0,
  });
  const emit = defineEmits(['nextPage', 'previousPage', 'toStart', 'toEnd']);

  const validateNextPage = () =>  props.currentPage >= props.totalPages;
  const validatePreviousPage = () =>  props.currentPage <= 1;

  const nextPage = () => {
    validateNextPage();
    emit('nextPage');
  };

  const prevPage = () => {
    validatePreviousPage();
    emit('previousPage');
  };

  const toStart = () => {
    validatePreviousPage();
    emit('toStart');
  };

  const toEnd = () => {
    validateNextPage()
    emit('toEnd');
  }
</script>

<template>
  <div class="flex flex-col items-center mt-4 select-none">
    <div class="flex justify-around w-full mb-2">
      <CustomButton @click="toStart"
                    :disabled="currentPage === 1"
                    class="disabled:opacity-50 disabled:line-through"
      >
        <<
      </CustomButton>
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
      <CustomButton @click="toEnd"
                    :disabled="currentPage === totalPages"
                    class="disabled:opacity-50 disabled:line-through"
      >
        >>
      </CustomButton>
    </div>
    <span class="text-text"
    >Page {{ currentPage }} of {{ totalPages }}</span
    >
  </div>
</template>

<style scoped>

</style>