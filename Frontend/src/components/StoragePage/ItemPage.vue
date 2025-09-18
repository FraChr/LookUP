<script setup>
import { useRoute } from 'vue-router';
import { onMounted, ref, computed } from 'vue';
import { useNormalizeData } from '@/composable/useNormalizeData.js';
import { crudFactory } from '@/Services/crudFactory.js';
import { useExcludeKeys } from '@/composable/useExcludeKeys.js';
import Select from '@/Select.vue';
import CustomButton from "@/components/CustomDefaultElements/CustomButton.vue";
import CustomInput from "@/components/CustomDefaultElements/CustomInput.vue";

const { numberInput, toUpperCase, dateFormat } = useNormalizeData();

const storage = crudFactory.useStorage();
const locations = crudFactory.useLocation();

const route = useRoute();
const id = route.params.id;

const editingKey = ref(null);

let tempItem = ref({});

const numberInputs = computed(() => {
  return Object.entries(storage.item.value)
    .filter(([_, value]) => typeof value === 'number')
    .map(([key]) => key);
});

const filteredItem = useExcludeKeys(storage.item, ['id', 'locationId']);
const editable = useExcludeKeys(filteredItem, ['timestamp']);

const update = () => {
  const toSend = {
    name: storage.item.value.name,
    amount: storage.item.value.amount,
    locationId: Number(storage.item.value.locationId),
  };

  storage.updateItem(id, toSend);
};

const startEdit = (key) => {
  editingKey.value = key;
  tempItem.value.locationId = Number(storage.item.value.locationId);
  tempItem.value[key] = storage.item.value[key];
};

const cancelEdit = () => {
  editingKey.value = null;
  tempItem.value = {};
};

const confirmEdit = (key) => {
  storage.item.value.locationId = Number(tempItem.value.locationId);

  storage.item.value[key] = tempItem.value[key];
  update();
  editingKey.value = null;
  tempItem.value = {};
};

onMounted(() => {
  storage.getSingle(id);
  locations.getAll();
});
</script>

<template>
  <div class="flex flex-row justify-around">
    <div class="w-2xl flex flex-col border-2 space-y-3 rounded-2xl p-2 select-none">
      <h1 class="underline text-center font-bold text-2xl">{{ storage.item.name }}</h1>
      <div v-for="(value, key) in filteredItem" :key="key" class="flex gap-7">
        <p v-if="key !== 'locationId'" class="font-bold text-lg">{{ toUpperCase(key) }}:</p>

        <template v-if="editingKey === key">
          <template v-if="editingKey === 'location'">
            <Select v-model="tempItem.locationId" :options="locations.items.value" />
          </template>

          <template v-else>
            <CustomInput
              v-if="!numberInputs.includes(key)"
              v-model="tempItem[key]"
            />
            <CustomInput
                v-else
                @input="tempItem[key] = numberInput($event.target.value)"
                v-model="tempItem[key]"
            />
          </template>

          <CustomButton
            v-if="editingKey === key"
            @click="confirmEdit(key)"
          >
            Confirm
          </CustomButton>
          <CustomButton
            @click="cancelEdit"
          >
            Cancel
          </CustomButton>
        </template>

        <template v-else>
          <div class="flex justify-between w-full">
            <p class="mt-0.5">
              {{ key === 'timestamp' ? dateFormat(value) : value }}
            </p>
            <CustomButton v-if="Object.keys(editable).includes(key)"
                          @click="startEdit(key)"
                          :label="'Edit'">
              Edit
            </CustomButton>
          </div>
        </template>
      </div>
    </div>
  </div>
</template>

<style scoped></style>
