<script setup>
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import Search from '@/components/Search.vue';
import { crudFactory } from '@/Services/crudFactory.js';
import { useExcludeKeys } from '@/composable/useExcludeKeys.js';
import { useNormalizeData } from '@/composable/useNormalizeData.js';
import TableComp from '@/components/table/TableComp.vue';
import Select from '@/Select.vue';

const { dateFormat } = useNormalizeData();

const storage = crudFactory.useStorage();

const headers = useExcludeKeys(storage.items, ['id', 'locationId', 'shelfsId']);

const searchTerm = ref('');
const router = useRouter();

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
  if (storage.currentPage.value >= storage.totalPages.value) return;

  storage.currentPage.value++;
  storage.getAll(searchTerm.value ? searchTerm.value : undefined);
};

const prevPage = () => {
  if (storage.currentPage.value <= 1) return;

  storage.currentPage.value--;
  storage.getAll(searchTerm.value ? searchTerm.value : undefined);
};

const navigateToItem = (data) => {
  router.push({ path: `/storage/${data.id}` });
};



onMounted(() => {
  storage.getAll();
});
</script>

<template>

  <Search @search="handleSearch" />


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

  <div class="flex flex-col items-center mt-4 select-none">
    <div class="flex justify-around w-full mb-2">
      <button
        @click="prevPage"
        :disabled="storage.currentPage.value === 1"
        class="disabled:opacity-50 disabled:line-through border-3 rounded-full border-gray-400 text-gray-400 p-2 hover:text-black hover:border-black"
      >
        Previous
      </button>
      <button
        @click="nextPage"
        :disabled="storage.currentPage.value === storage.totalPages.value"
        class="disabled:opacity-50 disabled:line-through border-3 rounded-full border-gray-400 text-gray-400 p-2 hover:text-black hover:border-black"
      >
        Next
      </button>
    </div>
    <span class="text-gray-400"
      >Page {{ storage.currentPage.value }} of {{ storage.totalPages.value }}</span
    >
  </div>
</template>

<style scoped></style>
