<script setup>
import { useRoute } from 'vue-router';
import { getItemById, getRooms, updateItem } from '@/Services/api.js';
import { onMounted, ref, computed, watch } from 'vue';
import { useFetchRooms } from '@/composable/fetchRooms.js';
import { usePreventExponential } from '@/composable/preventExponential.js';

const { rooms, getRoomsData } = useFetchRooms();
const { preventExponential } = usePreventExponential();

const route = useRoute();
const id = route.params.id;
const item = ref({});
const editingKey = ref(null);

const excludeKeys = ['locationId', 'id'];

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

const fetchItem = async () => {
  try {
    const itemResponse = await getItemById(id);
    item.value = itemResponse.data;
  } catch (error) {
    console.error('Error fetching item', error);
  }
};

const update = async () => {
  try {
    const toSend = {
      name: item.value.name,
      amount: item.value.amount,
      locationId: Number(item.value.locationId),
    };

    const response = await updateItem(id, toSend);
    item.value = response.data;
  } catch (error) {
    console.error('Error updating item', error.message, '|', error.detail);
  }
};

const startEdit = (key) => {
  editingKey.value = key;
  tempItem.value[key] = item.value[key];
  if (key === 'location') {
    tempItem.value['locationId'] = Number(item.value['locationId']);
  }
};

const cancelEdit = () => {
  editingKey.value = null;
  tempItem.value = {};
};

const confirmEdit = (key) => {
  if (key === 'location') {
    item.value['locationId'] = Number(tempItem.value['locationId']);
  } else {
    item.value[key] = tempItem.value[key];
  }
  update();
  editingKey.value = null;
  tempItem.value = {};
};

onMounted(() => {
  fetchItem();
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
