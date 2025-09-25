<script setup>
import { useRoute } from 'vue-router';
import { onMounted, ref, computed } from 'vue';
import { useNormalizeData } from '@/composable/useNormalizeData.js';
import { crudFactory } from '@/Services/crudFactory.js';
import { useExcludeKeys } from '@/composable/useExcludeKeys.js';
import Select from '@/Select.vue';
import CustomButton from '@/components/CustomDefaultElements/CustomButton.vue';
import CustomInput from '@/components/CustomDefaultElements/CustomInput.vue';
import ShowData from '@/components/UserPage/ShowData.vue';
import EditOptions from '@/components/EditOptions.vue';

const { numberInput, toUpperCase, dateFormat } = useNormalizeData();

const storage = crudFactory.useStorage();
const locations = crudFactory.useLocation();
const shelfs = crudFactory.useShelfs();

const route = useRoute();
const id = route.params.id;

const editingKey = ref(null);

let tempItem = ref({});

// let editing = ref({
//   editName: false,
//   editAmount: false,
//   editLocation: false,
//   editShelf: false,
// });
// const labels = {
//   editProfile: 'Edit Profile',
//   editRooms: 'Edit Rooms',
// };

const numberInputs = computed(() => {
  return Object.entries(storage.item.value)
    .filter(([_, value]) => typeof value === 'number')
    .map(([key]) => key);
});

const filteredItem = useExcludeKeys(storage.item, ['id', 'locationId', 'shelfsId']);
const editable = useExcludeKeys(filteredItem, ['timestamp']);

const update = () => {
  const toSend = {
    name: storage.item.value.name,
    amount: storage.item.value.amount,
    locationId: Number(storage.item.value.locationId),
    shelfsId: Number(storage.item.value.shelfsId),
  };

  storage.updateItem(id, toSend);
};

const startEdit = (key) => {
  editingKey.value = key;
  tempItem.value.locationId = Number(storage.item.value.locationId);
  tempItem.value.shelfsId = Number(storage.item.value.shelfsId);
  tempItem.value[key] = storage.item.value[key];
};

const cancelEdit = () => {
  editingKey.value = null;
  tempItem.value = {};
};

const confirmEdit = (key) => {
  storage.item.value.locationId = Number(tempItem.value.locationId);
  storage.item.value.shelfsId = Number(tempItem.value.shelfsId);

  storage.item.value[key] = tempItem.value[key];
  console.log('storage.item.value', storage.item.value);
  update();
  editingKey.value = null;
  tempItem.value = {};
};

onMounted(async() => {
  await storage.getSingle(id);
  await locations.getAll();
  await shelfs.getAll();
  console.log("item.value: ", storage.item.value);
});

</script>

<template>

  <div class="flex flex-row justify-around">
    <div class="w-2xl flex flex-col border-2 space-y-3 rounded-2xl p-2 select-none">
      <div v-for="(value, key) in filteredItem" :key="key" class="flex justify-between gap-7">

        <p class="font-bold text-lg">{{ toUpperCase(key) }}:</p>

        <div class="flex justify-between w-full" v-if="editingKey === key">


          <Select v-if="editingKey === 'location'"
                  v-model="tempItem.locationId"
                  :options="locations.items.value" />

          <Select v-else-if="editingKey === 'shelf'"
                  v-model="tempItem.shelfsId"
                  :options="shelfs.items.value" />

          <CustomInput v-else-if="numberInputs.includes(key)" v-model="tempItem[key]"
                    @input="tempItem[key] = numberInput($event.target.value)">
          </CustomInput>

          <CustomInput v-else v-model="tempItem[key]"></CustomInput>

          <CustomButton  @click="confirmEdit(key)"> Confirm </CustomButton>
          <CustomButton @click="cancelEdit"> Cancel </CustomButton>
        </div>

        <div v-else class="flex justify-between w-full">
          <ShowData :value="key === 'timestamp' ? dateFormat(value) : value"> </ShowData>
          <CustomButton
            v-if="Object.keys(editable).includes(key)"
            @click="startEdit(key)"
            :label="'Edit'"
          >
            Edit
          </CustomButton>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped></style>
