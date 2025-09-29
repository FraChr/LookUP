<script setup>
  import { onMounted, ref } from 'vue'
  import {crudFactory} from '@/Services/crudFactory.js';
  import TableComp from '@/components/table/TableComp.vue';
  import {useExcludeKeys} from '@/composable/useExcludeKeys.js';
  import EditOptions from '@/components/EditOptions.vue';
  import CustomInput from '@/components/CustomDefaultElements/CustomInput.vue';
  import CustomButton from '@/components/CustomDefaultElements/CustomButton.vue';
  import ProfileLayout from '@/components/UserPage/ProfileLayout.vue';
  import Popup from '@/components/Popup.vue';
  import ConfirmDelete from '@/components/ConfirmDelete.vue';

  const shelfs = crudFactory.useShelfs();
  const headers = useExcludeKeys(shelfs.items, ['id']);

  const shelfName = ref('');
  const showTable = ref(false);

  let editing = ref({
    editShelf: false,
  });
  const labels = {
    editShelf: 'Edit Shelf',
  };

  const showConfirmDelete = ref(false);
  let selectedEntity = ref({});

  function toggleTable() {
    showTable.value = !showTable.value;
  }

  async function addShelf() {
    if(shelfName.value === '') return;
    await shelfs.addItem({Name: shelfName.value});
    await shelfs.getAll();
    shelfName.value = '';
  }

  async function removeShelf(data) {
    await shelfs.deleteItem(data.id);
    await shelfs.getAll()
  }

  onMounted(async () => {
    await shelfs.getAll();
  });
</script>

<template>
  <ProfileLayout>
    <template #Right>
      <EditOptions v-model="editing" :labels="labels" @confirm="addShelf">
        <template #customActions="{keyName, editing}">
          <CustomButton v-if="keyName === 'editShelf'" @click="toggleTable">{{showTable ? 'Hide Shelf\'s' : 'Show Shelf\'s' }}</CustomButton>
        </template>
      </EditOptions>
    </template>

    <template #Left>
        <form v-if="editing.editShelf === true" @submit.prevent="addShelf">
          <label>Add Shelf</label>
          <CustomInput v-model="shelfName" maxlength="50" placeholder="Shelf Name" />
        </form>

        <TableComp class="min-w-sm flex-shrink" v-if="showTable" :data="shelfs.items" :headers="headers">
          <template #extraHeaders>
            <th></th>
          </template>
          <template #extraColumns="{ entity }">
            <td class="flex justify-end p-2">
              <CustomButton
                class="border-primary"
                @click="() => {
                  selectedEntity.value = entity;
                  showConfirmDelete = true;
                }"
              >
                Delete
              </CustomButton>
            </td>
          </template>
        </TableComp>
    </template>
  </ProfileLayout>

  <Popup :visible="showConfirmDelete">
    <ConfirmDelete :show-confirm-delete="true"
                   :message="`Warning: DELETE SHELF ${selectedEntity.value?.name}?`"
                    @confirmDelete="() => {
                      removeShelf(selectedEntity.value);
                      showConfirmDelete = false;
                    }"
                   @cancelDelete="() => {
                     selectedEntity.value = {};
                     showConfirmDelete = false;
                   }"
    ></ConfirmDelete>
  </Popup>
</template>

<style scoped>

</style>