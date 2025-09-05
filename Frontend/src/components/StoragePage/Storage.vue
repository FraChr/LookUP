<script setup>
import { ref, onMounted } from 'vue';
import { deleteItem } from '@/Services/api.js';
import { useRouter } from 'vue-router';
import Search from '@/components/Search.vue';
import { useFetch } from '@/composable/fetch.js';

const headers = ['name', 'amount', 'location'];

const searchTerm = ref('');
const router = useRouter();

const { items, currentPage, totalPages, fetchItems } = useFetch();

const handleSearch = async (term) => {
  searchTerm.value = term;
  currentPage.value = 1;
  await fetchItems(term);
};

const removeItem = async (id) => {
  try {
    await deleteItem(id);
    await fetchItems();
  } catch (error) {
    console.error(`Error deleting item: ${error}`);
  }
};

const nextPage = async () => {
  if (currentPage.value >= totalPages.value) return;

  currentPage.value++;
  await fetchItems(searchTerm.value ? searchTerm.value : undefined);
};

const prevPage = async () => {
  if (currentPage.value <= 1) return;

  currentPage.value--;
  await fetchItems(searchTerm.value ? searchTerm.value : undefined);
};

const navigateToItem = async (id) => {
  await router.push({ path: `/storage/${id}` });
};


onMounted(() => fetchItems());
console.log(`items.value ${items.value}`)

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
        <tr v-for="item in items" :key="item.id" class="border-b bg-gray-800 border-gray-700 group">
          <td
            v-for="(header, index) in headers"
            :key="header"
            @click="navigateToItem(item.id)"
            class="px-6 py-4 cursor-pointer group-hover:bg-gray-700 first:group-hover:rounded-l-2xl"
            :class="[index === headers.length - 1 ? 'group-hover:rounded-r-2xl' : '']"
          >
            {{ item[header] }}
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
        :disabled="currentPage === 1"
        class="disabled:opacity-50 disabled:line-through border-3 rounded-full border-gray-400 text-gray-400 p-2 hover:text-black hover:border-black"
      >
        Previous
      </button>
      <button
        @click="nextPage"
        :disabled="currentPage === totalPages"
        class="disabled:opacity-50 disabled:line-through border-3 rounded-full border-gray-400 text-gray-400 p-2 hover:text-black hover:border-black"
      >
        Next
      </button>
    </div>
    <span class="text-gray-400">Page {{ currentPage }} of {{ totalPages }}</span>
  </div>
</template>

<style scoped></style>
