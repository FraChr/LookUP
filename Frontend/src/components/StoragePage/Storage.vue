<script setup>
import { ref, onMounted, computed } from 'vue';
import { useRouter } from 'vue-router';
import Search from '@/components/Search.vue';
import {fetchFactory} from '@/Services/fetchFactory.js';
import {useDateFormat} from '@/composable/useDateFormat.js';
import { useExcludeKeys } from '@/composable/useExcludeKeys.js';


const storage = fetchFactory.useStorage();
// const headers = computed(() => {
//   const firstItem = storage.items.value[0];
//   return firstItem ? Object.keys(firstItem).filter(key => key !== 'id' && key !== 'locationId') : [];
// });

const headers = useExcludeKeys(storage.items, ['id', 'locationId']);

const searchTerm = ref('');
const router = useRouter();



const handleSearch =  (term) => {
  searchTerm.value = term;
  storage.currentPage.value = 1;
  storage.getAll(term);
};

const removeItem = (id) => {
  try {
    storage.deleteItem(id);
    storage.getAll();
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

const navigateToItem = (id) => {
  router.push({ path: `/storage/${id}` });
};


onMounted(() => storage.getAll());

</script>

<template>
  <Search @search="handleSearch" />
  <div class="z-40 flex justify-center items-center select-none">
    <table class="w-full text-sm text-left rtl:text-right text-gray-400">
      <thead class="text-xs uppercase bg-gray-700 text-gray-400">
        <tr>
          <th v-for="header in headers" :key="header" class="px-6 py-4">
            {{ header }}
          </th>
          <th></th>
        </tr>
      </thead>

      <tbody>
        <tr v-for="item in storage.items.value" :key="item.id" class="border-b bg-gray-800 border-gray-700 group">
          <td
            v-for="(header, index) in headers"
            :key="header"
            @click="navigateToItem(item.id)"
            class="px-6 py-4 cursor-pointer group-hover:bg-gray-700 first:group-hover:rounded-l-2xl"
            :class="[index === headers.length - 1 ? 'group-hover:rounded-r-2xl' : '']"
          >
            {{ header === 'timestamp' ? useDateFormat(item[header]) : item[header] }}
          </td>
          <td>
            <button
              @click="removeItem(item.id)"
              class="hover:cursor-pointer hover:text-white border p-2 rounded-full"
            >
              Delete
            </button>
          </td>
        </tr>
      </tbody>
    </table>
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
    <span class="text-gray-400">Page {{ storage.currentPage.value }} of {{ storage.totalPages.value }}</span>
  </div>
</template>

<style scoped></style>
