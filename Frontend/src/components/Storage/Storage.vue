<script setup>
import { ref, onMounted, watch } from 'vue';
import { useRouter } from 'vue-router';
import Search from '@/components/Search.vue';
import { crudFactory } from '@/Services/crudFactory.js';
import { useExcludeKeys } from '@/composable/useExcludeKeys.js';
import { useNormalizeData } from '@/composable/useNormalizeData.js';
import TableComp from '@/components/table/TableComp.vue';
import Select from '@/Select.vue';
import Paging from '@/components/Storage/Paging.vue'

const { dateFormat } = useNormalizeData();

const storage = crudFactory.useStorage();

const headers = useExcludeKeys(storage.items, ['id', 'locationId', 'shelfsId']);

const searchTerm = ref('');
const router = useRouter();

const update = async () => storage.getAll(searchTerm.value ? searchTerm.value : undefined);

const handleSearch = (term) => {
  searchTerm.value = term;
  storage.currentPage.value = 1;
  storage.getAll(term);
};

const removeItem = async (data) => {
  try {
    await storage.deleteItem(data.id);
    await storage.getAll();
  } catch (error) {
    console.error(`Error deleting item: ${error}`);
  }
};

const nextPage = () => {
  storage.currentPage.value++;
  update();
};

const prevPage = () => {
  storage.currentPage.value--;
  update();
};
const toStart = () => {
  storage.currentPage.value = 1;
  update();
}
const toEnd = () => {
  storage.currentPage.value = storage.totalPages.value;
  update();
}
const navigateToItem = (data) => {
  router.push({ path: `/storage/${data.id}` });
};

onMounted(() => {
  storage.getAll();
});
</script>

<template>

  <Search @search="handleSearch" @clearSearch="searchTerm" />


  <div class="z-40 flex justify-center items-center select-none">
    <TableComp :headers="headers" :data="storage.items" @row-click="navigateToItem">
      <template #extraHeaders>
        <th></th>
      </template>
      <template #column4="{ entity }">
        {{ dateFormat(entity.timestamp) }}
      </template>
      <template #extraColumns="{ entity }">
        <td>
          <button
            class="hover:cursor-pointer hover:text-white border p-2 rounded-full"
            @click="removeItem(entity)"
          >
            Delete
          </button>
        </td>
      </template>
    </TableComp>
  </div>

  <Paging :currentPage="storage.currentPage.value"
          :totalPages="storage.totalPages.value"
          @nextPage="nextPage"
          @previousPage="prevPage"
          @toStart="toStart"
          @toEnd="toEnd">
  </Paging>
</template>

<style scoped></style>
