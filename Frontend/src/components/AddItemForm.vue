<script setup>

import { addItem, getRooms } from '@/Services/api.js'
  import { onMounted, ref } from 'vue'

  const rooms = ref([]);
  const selected = ref("");
  const tag = ref("");
  const amount = ref(0);
  const errorMsg = ref("");
  let isDisabled = ref(true);

  const getLocation = async () => {
    const response = await getRooms();
    rooms.value = response.data.data;
  }

  const add = async () => {

    errorMsg.value = "";

    try{
    const item = {
      Name: tag.value,
      Amount: Number(amount.value),
      LocationId: Number(selected.value)
    };


    await addItem(item);

    selected.value = "";
    tag.value = "";
    amount.value = 0;

    }catch(error){
      console.error("error response: ", error.response.data)
      errorMsg.value =
        error.response?.data?.message ||
        error.message
    }
  }

  onMounted(() => {getLocation()});
</script>

<template>
  <div class="w-full flex flex-col items-center m-4">
    <form @submit.prevent="add" class="border-2 w-full max-w-md flex flex-col space-y-3 p-6">
      <select v-model="selected" class="border-2 p-2" >
        <option value="" disabled>Select Room</option>
        <option v-for="room in rooms" :key="room.id" :value="room.id">{{room.name}}</option>
      </select>
      <input v-model="tag" type="text" placeholder="Item Tag" class="border-2 p-2" required/>
      <input v-model="amount" type="number" placeholder="Amount" class="border-2 p-2"/>
      <div class="flex justify-center">
        <button type="submit"  class="hover:bg-white bg-amber-200 border-2 px-6 py-2">Add</button>
      </div>
    </form>
    <div v-if="errorMsg" class="text-red-500 font-semibold p-2 text-center">
      {{errorMsg}}
    </div>
  </div>
</template>

<style scoped>

</style>