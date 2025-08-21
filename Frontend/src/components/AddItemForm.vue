<script setup>

import { addItem, getRooms } from '@/Services/api.js'
  import { onMounted, ref } from 'vue'

  const rooms = ref([]);
  const selected = ref("");
  const tag = ref("");
  const amount = ref(0);
  let isDisabled = ref(true);

  const getLocation = async () => {
    const response = await getRooms();
    rooms.value = response.data.data;
  }

  const add = async () => {
    try{
    const item = {
      Name: tag.value,
      Amount: Number(amount.value),
      Location: String(selected.value)
    };


    const response = await addItem(item);
    console.log(response);

    console.log(`you added: to location: ${selected.value}\nWith Tag: ${tag.value}\nWith Amount: ${amount.value}`);

    selected.value = "";
    tag.value = "";
    amount.value = 0;

    }catch(error){
      console.log("Error: ",error);
    }



  }

  onMounted(() => {getLocation()});
</script>

<template>
  <div class="border-2 flex flex-col w-1/2 space-y-3 m-4">
    <select v-model="selected" class="border-2" required>
      <option value="" disabled>Select Room</option>
      <option v-for="room in rooms" :key="room.id" :value="room.id">{{room.name}}</option>
    </select>
    <input v-model="tag" type="text" placeholder="Item Tag" class="border-2" required/>
    <input v-model="amount" type="number" placeholder="Amount" class="border-2" required/>
    <div class="flex justify-center">
      <button @click="add" class="hover:bg-white bg-amber-200 border-2 w-1/5">Add</button>
    </div>
  </div>
</template>

<style scoped>

</style>