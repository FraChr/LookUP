<script setup>
import { onMounted, ref, watch } from 'vue';
import { crudFactory } from '@/Services/crudFactory.js';
import TableComp from '@/components/table/TableComp.vue';
import { useExcludeKeys } from '@/composable/useExcludeKeys.js';
import ProfileOptions from '@/components/ProfileOptions.vue';
import CustomInput from '@/components/CustomDefaultElements/CustomInput.vue';

const rooms = crudFactory.useLocation();
const headers = useExcludeKeys(rooms.items, ['id']);

const roomName = ref('');

let editing = ref({
  editRooms: false,
});
const labels = {
  editRooms: 'Edit Rooms',
};

async function removeRoom(data) {
  console.log('REMOVED ROOM');
  await rooms.deleteItem(data.id);
  await rooms.getAll();
}
async function addRoom() {
  console.log('UPDATED ROOM');
  if (roomName.value === '') return;
  await rooms.addItem({Name: roomName.value});
  await rooms.getAll();
  roomName.value = '';
}
watch(headers, (newVal) => {
  console.log("headers value", newVal);
});
onMounted(async () => {
  await rooms.getAll();
});
</script>

<template>
  <div class="grid grid-cols-2 gap-4 w-full">
    <div>
      <ProfileOptions v-model="editing" :labels="labels" @confirm="addRoom"> </ProfileOptions>
    </div>

    <div class="flex flex-col border-2">
      <form v-if="editing.editRooms === true" @submit.prevent="addRoom">
        <label>Add Room</label>
        <CustomInput v-model="roomName" placeholder="Room Name"></CustomInput>
      </form>

      <TableComp :data="rooms.items" :headers="headers">
        <template #extraHeaders>
          <th></th>
        </template>
        <template #extraColumns="{ entity }">
          <td>
            <button
              class="hover:cursor-pointer hover:text-white border p-2 rounded-full"
              @click="removeRoom(entity)"
            >
              Delete
            </button>
          </td>
        </template>
      </TableComp>
    </div>
  </div>
</template>

<style scoped></style>
