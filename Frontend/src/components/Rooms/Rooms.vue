<script setup>
import { onMounted, ref } from 'vue';
import { crudFactory } from '@/Services/crudFactory.js';
import TableComp from '@/components/table/TableComp.vue';
import { useExcludeKeys } from '@/composable/useExcludeKeys.js';
import EditOptions from '@/components/EditOptions.vue';
import CustomInput from '@/components/CustomDefaultElements/CustomInput.vue';
import CustomButton from '@/components/CustomDefaultElements/CustomButton.vue';

const rooms = crudFactory.useLocation();
const headers = useExcludeKeys(rooms.items, ['id']);

const roomName = ref('');
const showTable = ref(false);

let editing = ref({
  editRooms: false,
});
const labels = {
  editRooms: 'Edit Rooms',
};

function toggleTable() {
  showTable.value = !showTable.value;
}

async function removeRoom(data) {
  await rooms.deleteItem(data.id);
  await rooms.getAll();
}
async function addRoom() {
  if (roomName.value === '') return;
  await rooms.addItem({Name: roomName.value});
  await rooms.getAll();
  roomName.value = '';
}

onMounted(async () => {
  await rooms.getAll();
});
</script>

<template>
  <div class="grid grid-cols-2 gap-4 w-full">
    <div>
      <EditOptions v-model="editing" :labels="labels" @confirm="addRoom">
        <template #customActions="{keyName, editing}">
          <CustomButton v-if="keyName === 'editRooms'" @click="toggleTable">{{showTable ? 'Hide Table' : 'Show Tables' }}</CustomButton>
        </template>
      </EditOptions>
    </div>

    <div class="flex flex-col gap-4">
      <form v-if="editing.editRooms === true" @submit.prevent="addRoom">
        <label>Add Room</label>
        <CustomInput v-model="roomName" placeholder="Room Name"></CustomInput>
      </form>

      <TableComp class="min-w-sm" v-if="showTable" :data="rooms.items" :headers="headers">
        <template #extraHeaders>
          <th></th>
        </template>
        <template #extraColumns="{ entity }">
          <td class="flex justify-end p-2">
            <CustomButton
              class="border-primary"
              @click="removeRoom(entity)"
            >
              Delete
            </CustomButton>
          </td>
        </template>
      </TableComp>
    </div>
  </div>
</template>

<style scoped></style>
