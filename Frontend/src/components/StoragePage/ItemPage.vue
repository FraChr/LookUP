<script setup>
  import { useRoute } from 'vue-router'
  import { getItemById, getRooms, updateItem } from '@/Services/api.js'
  import { onMounted, ref, computed } from 'vue'

  const route = useRoute();
  const id = route.params.id;
  const item = ref({});
  const itemName = ref("");
  const editingKey = ref(null);
  const roomData = ref([]);

  const selected = ref("");


  const excludeKeys = ["locationId", "id"];

  const filteredItemEnteries = computed(() => {
    return Object.entries(item.value).filter(([key]) => !excludeKeys.includes(key));
  })


  const fetchItems = async () => {
    try {
      const roomResponse = await getRooms();
      const itemResponse = await getItemById(id);
      item.value = itemResponse.data;
      roomData.value = roomResponse.data.data;

      // console.log("rooms",roomData.value.data);

    }catch (error) {
      console.log("Error fetching item", error);
    }
  }

  const update = async () => {
    try{
      const toSend = {
        name: item.value.name,
        amount: item.value.amount,
        location: String(item.value.locationId)
      }


      const response = await updateItem(id, toSend);
      console.log("response from update: ", response.data);
    }catch (error) {
      console.log("Error updating item", error.message ,"|", error.detail);
    }
  }


  const confirmEdit = (key) => {
    console.log(`Confirmed edit for ${key}: `, item.value[key]);
    console.log("data to send: ", item.value)
    update();
    editingKey.value = null;
  }

 onMounted(() => fetchItems());
</script>

<template>
<!--  grid grid-cols-2 gap-4-->
  <div class="flex flex-row justify-around">
    <div class="border 2">
      {{item.locationId}}
    </div>

    <div class=" w-full flex flex-col border-2 space-y-3 rounded-2xl p-2 select-none">
      <h1 class="underline font-bold text-2xl">{{itemName}}</h1>
      <div v-for="[key, value] in filteredItemEnteries" :key="key" class="flex justify-between">
        <span v-if="key !== 'locationId'">{{key}}:</span>
        <span v-if="editingKey !== key"
              @click="editingKey = key"
              class="cursor-pointer">
          {{value}}
        </span>
        <input v-else
               v-model="item[key]"

               class="border border-gray-300 px-2 py-1 rounded"
        />

        <button v-if="editingKey === key"
                @click="confirmEdit(key)"
                class="cursor-pointer">Confirm</button>
<!--        <button @click="test">Edit</button>-->
      </div>
<!--      <span v-for="(value, key) in item">{{key}}: {{value}}</span>-->
    </div>

  </div>
  <select v-model="selected" class="border-2 p-2" >
    <option value="" disabled>Select Room</option>
    <option v-for="room in roomData" :key="room.id" :value="room.id">{{room.name}}</option>
  </select>
</template>

<style scoped>

</style>