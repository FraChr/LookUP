<script setup>
import { useRoute } from 'vue-router';
// import { getItemById, getRooms, updateItem } from '@/Services/api.js';
import { onMounted, ref, computed, watch } from 'vue';
import { useFetchRooms } from '@/composable/useFetchRooms.js';
import { usePreventExponential } from '@/composable/usePreventExponential.js';

import {fetchFactory} from '@/Services/fetchFactory.js';


const { rooms, getRoomsData } = useFetchRooms();
const { preventExponential } = usePreventExponential();

const {getSingle, item, updateItem} = fetchFactory.useStorage()

const route = useRoute();
const id = route.params.id;

const editingKey = ref(null);

const excludeKeys = ['id'];

let tempItem = ref({});

const numberInputs = computed(() => {
  return Object.entries(item.value)
    .filter(([_, value]) => typeof value === 'number')
    .map(([key]) => key);
});

const filteredItemEntries = computed(() => {
  return Object.entries(item.value).reduce((acc, [key, value]) => {
    if (!excludeKeys.includes(key)) {
      acc[key] = value;
    }
    return acc;
  }, {});
});

const update = () => {
    const toSend = {
      name: item.value.name,
      amount: item.value.amount,
      locationId: Number(item.value.locationId),
    };

    updateItem(id, toSend);
    console.log("ITEM ", item.value);

};

const startEdit = (key) => {
  editingKey.value = key;

  const matchRoom = rooms.value.find(room => room.name === item.value.location);
  tempItem.value['locationId'] = matchRoom?.id ?? null;

  tempItem.value[key] = item.value[key];

  console.log("TEMP ITEM",tempItem.value.locationId);
};

const cancelEdit = () => {
  editingKey.value = null;
  tempItem.value = {};
};

const confirmEdit = (key) => {
  item.value['locationId'] = Number(tempItem.value['locationId']);

  item.value[key] = tempItem.value[key];

  console.log("TEMP ITEM",tempItem.value);
  console.log("Item ", item.value);
  update();
  editingKey.value = null;
  tempItem.value = {};
};


onMounted(() => {
  getSingle(id);
  getRoomsData();
});
</script>

<template>
  <div class="flex flex-row justify-around">
    <div class="w-2xl flex flex-col border-2 space-y-3 rounded-2xl p-2 select-none">
      <h1 class="underline text-center font-bold text-2xl">{{ item.name }}</h1>
      <div v-for="(value, key) in filteredItemEntries" :key="key" class="flex gap-7">
        <p v-if="key !== 'locationId'" class="font-bold text-lg">{{ key }}:</p>

        <template v-if="editingKey === key">
          <template v-if="editingKey === 'location'">
            <select v-model="tempItem.locationId" class="border-2 p-2">
              <option v-for="room in rooms" :key="room.id" :value="room.id">
                {{ room.name }}
              </option>
            </select>
          </template>

          <template v-else>
            <input
              v-if="!numberInputs.includes(key)"
              v-model="tempItem[key]"
              class="border border-gray-300 px-2 py-1 rounded"
            />
            <input
              v-else
              @input="tempItem[key] = preventExponential($event.target.value)"
              v-model="tempItem[key]"
              class="border border-gray-300 px-2 py-1 rounded"
            />
          </template>

          <button
            v-if="editingKey === key"
            @click="confirmEdit(key)"
            class="cursor-pointer border-2 p-2 rounded-2xl hover:bg-amber-300"
          >
            Confirm
          </button>
          <button
            @click="cancelEdit"
            class="cursor-pointer border-2 p-2 rounded-2xl hover:bg-amber-300"
          >
            Cancel
          </button>
        </template>

        <template v-else>
          <div class="flex justify-between w-full">
            <p class="mt-0.5">
              {{ value }}
            </p>
            <button @click="startEdit(key)" class="cursor-pointer border-2 rounded-2xl p-2">
              Edit
            </button>
          </div>
        </template>
      </div>
    </div>
  </div>
</template>

<style scoped></style>
