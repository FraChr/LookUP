<script setup>

  import { getRooms } from '@/Services/api.js'
  import { onMounted, ref } from 'vue'

  const rooms = ref([]);

  const getLocation = async () => {
    const response = await getRooms();
    rooms.value = response.data;
  }

  const addItem = async () => {

  }

  onMounted(() => {getLocation()});
</script>

<template>
  <div class="border-2 flex flex-col w-1/2 space-y-3 m-4">
    <select class="border-2" required>
      <option value="" disabled selected>Select Room</option>
      <option v-for="room in rooms" :key="room.id" :value="room">{{room.name}}</option>
    </select>
    <input type="text" placeholder="Item Tag" class="border-2" required/>
    <input type="number" placeholder="Amount" class="border-2" required/>
    <div class="flex justify-center">
      <button @click="addItem" class="border-2 w-1/5">Add</button>
    </div>
  </div>
</template>

<style scoped>

</style>