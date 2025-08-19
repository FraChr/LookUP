<script setup>
  import axios from 'axios';
  import { ref, onMounted } from 'vue'
  import { getStorage, deleteItem } from '@/Services/api.js'

    const item = ref([])
    const headers = ['name', 'amount', 'location']



  onMounted(() => fetchItems())

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
      const response = await getStorage();
      item.value = response.data;
    }catch(error){
      console.error(`Error fetching data ${error}`);
    }
  }

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
        <tr v-for="user in item" :key="user.id" class="bg-white border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200">
          <td v-for="header in headers" :key="header" class="px-6 py-4">
            {{user[header]}}
          </td>
          <td>
            <button @click="removeItem(user.id)" class="hover:cursor-pointer
            hover:text-white border p-2 rounded-full">
              Delete
            </button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<style scoped>

</style>