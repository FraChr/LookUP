<script setup>
// import { addItem } from '@/Services/api.js';
import { onMounted, ref } from 'vue';

import { usePreventExponential } from '@/composable/usePreventExponential.js';

import { fetchFactory } from '@/Services/fetchFactory.js';

const storage = fetchFactory.useStorage();
const location = fetchFactory.useLocation();

const selected = ref('');
const tag = ref('');
const amount = ref(0);
const errorMsg = ref('');

const { preventExponential } = usePreventExponential();

const add = () => {
  errorMsg.value = '';

  try {
    const item = {
      Name: tag.value,
      Amount: Number(amount.value),
      LocationId: Number(selected.value),
    };

    console.log("ITEM ",item);

     storage.addItem(item);

    selected.value = '';
    tag.value = '';
    amount.value = 0;
  } catch (error) {
    console.error('error response: ', error.response.data);
    errorMsg.value = error.response?.data?.message || error.message;
  }
};


onMounted( () => {
  location.getAll();
});
</script>

<template>
  <div class="w-full flex flex-col items-center m-4">
    <form @submit.prevent="add" class="border-2 w-full max-w-md flex flex-col space-y-3 p-6">
      <select v-model="selected" class="border-2 p-2" required>
        <option value="" disabled>Select Room</option>
        <option v-for="room in location.items.value" :key="room.id" :value="room.id">{{ room.name }}</option>
      </select>
      <input v-model="tag" type="text" placeholder="Item Tag" class="border-2 p-2" required />
      <input
        v-model="amount"
        type="text"
        @input="amount = preventExponential($event.target.value)"
        placeholder="Amount"
        class="border-2 p-2"
      />


      <div class="flex justify-center">
        <button type="submit" class="hover:bg-white bg-amber-200 border-2 px-6 py-2">Add</button>
      </div>
    </form>
    <div v-if="errorMsg" class="text-red-500 font-semibold p-2 text-center">
      {{ errorMsg }}
    </div>
  </div>
</template>

<style scoped></style>
