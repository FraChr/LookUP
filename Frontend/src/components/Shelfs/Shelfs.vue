<script setup>
  import { onMounted, ref } from 'vue'
  import {crudFactory} from '@/Services/crudFactory.js';
  import TableComp from '@/components/table/TableComp.vue';
  import {useExcludeKeys} from '@/composable/useExcludeKeys.js';
  import ProfileOptions from '@/components/ProfileOptions.vue';
  import CustomInput from '@/components/CustomDefaultElements/CustomInput.vue';
  import CustomButton from '@/components/CustomDefaultElements/CustomButton.vue';

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
    console.log("Shelfs items: ", shelfs.items);
  });
</script>

<template>
  <div class="grid grid-cols-2 gap-4 w-full">
    <div>
      <ProfileOptions v-model="editing" :labels="labels" @confirm="addShelf">
        <template #customActions="{keyName, editing}">
          <CustomButton v-if="keyName === 'editShelf'" @click="toggleTable">{{showTable ? 'Hide Table' : 'Show Tables' }}</CustomButton>
        </template>
      </ProfileOptions>
    </div>
    <div class="flex flex-col">
      <form v-if="editing.editShelf === true" @submit.prevent="addShelf">
        <label>Add Shelf</label>
        <CustomInput v-model="shelfName" maxlength="50" placeholder="Shelf Name" />
      </form>

      <TableComp v-if="showTable" :data="shelfs.items" :headers="headers">
        <template #extraHeaders>
          <th></th>
        </template>
        <template #extraColumns="{ entity }">
          <td>
            <button
              class="hover:cursor-pointer hover:text-white border p-2 rounded-full"
              @click="removeShelf(entity)"
            >
              Delete
            </button>
          </td>
        </template>
      </TableComp>
    </div>
  </div>
</template>

<style scoped>

</style>