<script setup>
import { onMounted, ref, watch } from 'vue';
import { crudFactory } from '@/Services/crudFactory.js';
import TableComp from '@/components/table/TableComp.vue';
import { useExcludeKeys } from '@/composable/useExcludeKeys.js';
import EditOptions from '@/components/EditOptions.vue';
import CustomInput from '@/components/CustomDefaultElements/CustomInput.vue';
import CustomButton from '@/components/CustomDefaultElements/CustomButton.vue';
import ProfileLayout from '@/components/UserPage/ProfileLayout.vue';
import Popup from '@/components/Popup.vue';
import ConfirmDelete from '@/components/ConfirmDelete.vue';

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
const showConfirmDelete = ref(false);
let selectedEntity = ref({});

function toggleTable() {
  showTable.value = !showTable.value;
}

async function removeRoom(data) {
  await rooms.deleteItem(data.id);
  await rooms.getAll();
}
async function addRoom() {
  if (roomName.value === '') return;
  await rooms.addItem({ Name: roomName.value });
  await rooms.getAll();
  roomName.value = '';
}

watch(selectedEntity, (newEntity, oldEntity) => {
  console.log("NEW ENTITY FROM DELETE", typeof newEntity);
})

onMounted(async () => {
  await rooms.getAll();
});
</script>

<template>
  <ProfileLayout>
    <template #Right>
      <EditOptions v-model="editing" :labels="labels" @confirm="addRoom">
        <template #customActions="{ keyName, editing }">
          <CustomButton v-if="keyName === 'editRooms'" @click="toggleTable">{{
            showTable ? 'Hide Rooms' : 'Show Rooms'
          }}</CustomButton>
        </template>
      </EditOptions>
    </template>

    <template #Left>
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
              @click="
                () => {
                  selectedEntity.value = entity;
                  showConfirmDelete = true;
                }
              "
            >
              Delete
            </CustomButton>
          </td>
        </template>
      </TableComp>
    </template>
  </ProfileLayout>

  <Popup :visible="showConfirmDelete">
    <ConfirmDelete
      :show-confirm-delete="true"
      :message="`Warning: DELETE ROOM '${selectedEntity.value?.name}'?`"
      @confirmDelete="
        () => {
          removeRoom(selectedEntity.value);
          showConfirmDelete = false;
        }
      "
      @cancelDelete="
        () => {
          selectedEntity.value = {};
          showConfirmDelete = false;
        }
      "
    ></ConfirmDelete>
  </Popup>
</template>

<style scoped></style>
