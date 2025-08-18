<script setup>
  import Header from '../Header.vue'
  import axios from 'axios';
  import { ref, onMounted } from 'vue'

    const users = ref([])
    const headers = ['name', 'amount', 'location']

    onMounted(() => {
      axios.get('http://localhost:5223/storage')
      .then((response) => {
        users.value = response.data;
      })
        .catch((error) => {
          console.log('Error fetching data', error);
        });
    })


  const removeItem = () => {
    console.log("Hello World!!!");
  }

</script>

<template>
  <div class="flex justify-center items-center">
    <table class="w-full text-sm text-left rtl:text-right text-gray-500 dark:text-gray-400">
      <thead class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
        <tr>
          <th v-for="header in headers" :key="header" class="px-6 py-4">{{header}}</th>
        </tr>
      </thead>
      <tbody >
        <tr v-for="user in users" :key="user.id" class="bg-white border-b dark:bg-gray-800 dark:border-gray-700 border-gray-200">
          <td v-for="header in headers" :key="header" class="px-6 py-4">
            {{user[header]}}

          </td>
          <td>
            <button @click="removeItem">test</button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<style scoped>

</style>