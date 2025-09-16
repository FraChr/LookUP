<script setup>
import { useRoute } from 'vue-router';
import { onMounted, ref, computed, watch } from 'vue';
import { usePreventExponential } from '@/composable/usePreventExponential.js';
import {fetchFactory} from '@/Services/fetchFactory.js';
import { useExcludeKeys } from '@/composable/useExcludeKeys.js';
import { useDateFormat } from '@/composable/useDateFormat.js';

const { preventExponential } = usePreventExponential();

const storage = fetchFactory.useStorage();
const location = fetchFactory.useLocation();

const route = useRoute();
const id = route.params.id;

const editingKey = ref(null);

let tempItem = ref({});

const numberInputs = computed(() => {
  return Object.entries(storage.item.value)
    .filter(([_, value]) => typeof value === 'number')
    .map(([key]) => key);
});




// const displayDate = useDateFormat(storage.item.value.timestamp);
const filteredItem = useExcludeKeys(storage.item, ['id']);
const editable = useExcludeKeys(filteredItem, ['timestamp']);


// watch(displayDate, (newVal) => {
//   console.log("DATE: ", newVal);
// })



const update = () => {
    const toSend = {
      name: storage.item.value.name,
      amount: storage.item.value.amount,
      locationId: Number(storage.item.value.locationId),
    };

  storage.updateItem(id, toSend);
    console.log("ITEM ", storage.item.value);

};

const startEdit = (key) => {
  editingKey.value = key;

  const matchRoom = location.items.value.find(room => room.name === storage.item.value.location);
  tempItem.value['locationId'] = matchRoom?.id ?? null;

  tempItem.value[key] = storage.item.value[key];

  console.log("TEMP ITEM",tempItem.value.locationId);
};

const cancelEdit = () => {
  editingKey.value = null;
  tempItem.value = {};
};

const confirmEdit = (key) => {
  storage.item.value['locationId'] = Number(tempItem.value['locationId']);

  storage.item.value[key] = tempItem.value[key];

  console.log("TEMP ITEM",tempItem.value);
  console.log("Item ", storage.item.value);
  update();
  editingKey.value = null;
  tempItem.value = {};
};

onMounted(() => {
  storage.getSingle(id);
  location.getAll();
});
</script>

<template>
  <div class="flex flex-row justify-around">
    <div class="w-2xl flex flex-col border-2 space-y-3 rounded-2xl p-2 select-none">
      <h1 class="underline text-center font-bold text-2xl">{{ storage.item.name }}</h1>
      <div v-for="(value, key) in filteredItem" :key="key" class="flex gap-7">
        <p v-if="key !== 'locationId'" class="font-bold text-lg">{{ key.charAt(0).toUpperCase() + key.slice(1) }}:</p>

        <template v-if="editingKey === key && key !== 'timestamp'">
          <template v-if="editingKey === 'location'">
            <select v-model="tempItem.locationId" class="border-2 p-2">
              <option v-for="room in location.items.value" :key="room.id" :value="room.id">
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
              {{ key === 'timestamp' ? useDateFormat(value) : value }}
            </p>
            <button v-if="Object.keys(editable).includes(key)" @click="startEdit(key)" class="cursor-pointer border-2 rounded-2xl p-2">
              Edit
            </button>
          </div>
        </template>
      </div>
    </div>
  </div>
</template>

<style scoped></style>
