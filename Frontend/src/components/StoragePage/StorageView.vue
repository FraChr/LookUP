<script setup>
  import { ref, onMounted, computed } from 'vue'
  import { getStorage, deleteItem } from '@/Services/api.js'

    const items = ref([])
    const headers = ['name', 'amount', 'location']
    const currentPage = ref(1);
    const pageSize = ref(10);
    const total = ref(0);
    const totalPages = computed(() => Math.ceil(total.value / pageSize.value));



  const removeItem = async (id) => {
    try {
      await deleteItem(id);
      await fetchItems();
    }catch(error){
      console.error(`Error deleting item: ${error}`);
    }
  };

  const fetchItems = async () => {
    try{
      const response = await getStorage({
        params: {
          limit: pageSize.value,
          page: currentPage.value,
        }
      });
      items.value = response.data.data;
      total.value = response.data.total;
      console.log(items.value);
    }catch(error){
      console.error(`Error fetching data ${error}`);
    }
  }

  const nextPage = async () => {
    if(currentPage.value < totalPages.value){
      currentPage.value++;
      await fetchItems();
    }
  }

  const prevPage = async () => {
    if(currentPage.value > 1){
      currentPage.value--;
      await fetchItems();
    }
  }

  onMounted(() => fetchItems())
</script>

<template>
  <div class="flex justify-center items-center">
    <table class="w-full text-sm text-left rtl:text-right text-gray-500 dark:text-gray-400">
      <thead class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
        <tr>
          <th v-for="header in headers" :key="header" class="px-6 py-4">{{header}}</th>
          <th></th>
        </tr>
      </thead>
      <tbody >
        <tr v-for="item in items" :key="item.id" class="bg-white border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200">
          <td v-for="header in headers" :key="header" class="px-6 py-4">
            {{item[header]}}
          </td>
          <td>
            <button @click="removeItem(item.id)" class="hover:cursor-pointer
            hover:text-white border p-2 rounded-full">
              Delete
            </button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
  <div class="flex flex-col items-center mt-4">
    <div class="flex justify-around w-full mb-2">
      <button @click="prevPage" :disabled="currentPage === 1" class="disabled:opacity-50 disabled:line-through border-2 rounded-full bg-amber-200 p-2">Previous</button>
      <button @click="nextPage" :disabled="currentPage === totalPages" class="disabled:opacity-50 disabled:line-through border-2 rounded-full bg-amber-200 p-2">Next</button>
    </div>
    <span>Page {{currentPage}} of {{totalPages}}</span>
  </div>
</template>

<style scoped>

</style>