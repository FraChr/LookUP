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
  import Paging from '@/components/Storage/Paging.vue';

  const shelfs = crudFactory.useShelfs();
  const headers = useExcludeKeys(shelfs.items, ['id']);

  const shelfName = ref('');
  const showTable = ref(false);
  const customPageSize = 5;

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

  async function nextPage() {
    shelfs.currentPage.value++;
    await shelfs.getAll();
  }

  async function prevPage() {
    shelfs.currentPage.value--;
    await shelfs.getAll();
  }

  async function toStart() {
    shelfs.currentPage.value = 1;
    await shelfs.getAll();
  }
  async function toEnd() {
    shelfs.currentPage.value = shelfs.totalPages.value;
    await shelfs.getAll();
  }

  async function removeShelf(data) {
    await shelfs.deleteItem(data.id);
    await shelfs.getAll()
  }

  onMounted(async () => {
    shelfs.pageSize.value = customPageSize;
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

        <div v-if="showTable">
        <TableComp class="min-w-sm flex-shrink" :data="shelfs.items" :headers="headers">
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
          <div class="flex justify-center min-w-sm ">
            <Paging :currentPage="shelfs.currentPage.value"
                    :totalPages="shelfs.totalPages.value"
                    @nextPage="nextPage"
                    @previousPage="prevPage"
                    @toStart="toStart"
                    @toEnd="toEnd" />
            </div>
        </div>
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