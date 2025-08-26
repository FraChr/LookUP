<script setup>
import { useRoute } from 'vue-router'
import { getItemById, getRooms, updateItem } from '@/Services/api.js'
import { onMounted, ref, computed, watch } from 'vue'

const route = useRoute()
const id = route.params.id
const item = ref({})
const editingKey = ref(null)

const roomData = ref()

const excludeKeys = ['locationId', 'id'];

const numberInputs = computed(() => {
  return Object.entries(item.value)
    .filter(([_, value]) => typeof value === 'number')
    .map(([key]) => key);
});

const filteredItemEntries = computed(() => {
  return Object.entries(item.value).reduce((acc, [key, value]) => {
    if (!excludeKeys.includes(key)) {
      acc[key] = value
    }
    return acc
  }, {})
})

watch(numberInputs, (newValue) => {
  console.log("help", numberInputs.value);
})




let tempItem = ref({})





const fetchItems = async () => {
  try {
    const roomResponse = await getRooms()
    const itemResponse = await getItemById(id)
    item.value = itemResponse.data
    roomData.value = roomResponse.data.data

    console.log('item.value: ', item.value)
    console.log('filteredItemEntries: ', filteredItemEntries.value)
  } catch (error) {
    console.log('Error fetching item', error)
  }
}



const update = async () => {
  try {
    const toSend = {
      name: item.value.name,
      amount: item.value.amount,
      locationId: Number(item.value.locationId),
    }

    const response = await updateItem(id, toSend)
    console.log('response from update: ', response.data)
    item.value = response.data
  } catch (error) {
    console.log('Error updating item', error.message, '|', error.detail)
  }
}

const startEdit = (key) => {
  editingKey.value = key;
  tempItem.value[key] = item.value[key];
  if(key === "location") {
    tempItem.value['locationId'] = Number(item.value['locationId']);
  }
  console.log("tempItem: ", tempItem.value);
}

const cancelEdit = () => {
  editingKey.value = null;
  tempItem.value = {};
}

const confirmEdit = (key) => {
  console.log(`Confirmed edit for ${key}: `, item.value[key])
  console.log('data to send: ', item.value)
  if(key === "location"){
    item.value['locationId'] = Number(tempItem.value['locationId'])
  } else{
    item.value[key] = tempItem.value[key]
  }
  update()
  editingKey.value = null
  tempItem.value = {};
}

const preventExponential = (event) => {
  if (event.key.length === 1 && isNaN(Number(event.key))) {
    event.preventDefault();
  }
}


const getRoomNameById = (id) => {
  console.log('Looking up room name for ID:', id)
  const room = roomData.value.find((r) => String(r.id) === String(id))
  return room ? room.name : 'Unknown'
}

onMounted(() => fetchItems())
</script>

<template>
  <div class="flex flex-row justify-around">
    <div class="border 2">
      {{ item.location }}
    </div>

    <div class="w-full flex flex-col border-2 space-y-3 rounded-2xl p-2 select-none">
      <h1 class="underline font-bold text-2xl">{{ item.name }}</h1>

      <div v-for="(value, key) in filteredItemEntries" :key="key" class="flex justify-between ">
        <span v-if="key !== 'locationId'">{{ key }}:</span>

        <template v-if="editingKey === key">
          <template v-if="editingKey === 'location'">
            <select v-model="tempItem.locationId" class="border-2 p-2">
              <option v-for="room in roomData" :key="room.id" :value="room.id">
                {{ room.name }}
              </option>
            </select>
          </template>

          <template v-else>
            <input v-if="!numberInputs.includes(key)" v-model="tempItem[key]" class="border border-gray-300 px-2 py-1 rounded" />
            <input v-else type="number" @keydown="preventExponential" v-model="tempItem[key]" class="border border-gray-300 px-2 py-1 rounded" />
          </template>

          <button v-if="editingKey === key" @click="confirmEdit(key)"
                  class="cursor-pointer border-2 p-2
                         rounded-2xl hover:bg-amber-300">
            Confirm
          </button>
          <button @click="cancelEdit" class="cursor-pointer border-2 p-2
                                             rounded-2xl hover:bg-amber-300">
            Cancel
          </button>
        </template>

        <span v-else @click="startEdit(key)" class="cursor-pointer border-2 rounded-2xl p-2">
          {{ value }}
        </span>
      </div>
    </div>
  </div>
</template>

<style scoped></style>