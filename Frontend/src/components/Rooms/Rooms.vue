<script setup>
  import { onMounted } from 'vue';
  import { crudFactory } from '@/Services/crudFactory.js';
  import TableComp from '@/components/table/TableComp.vue';
  import { useExcludeKeys } from '@/composable/useExcludeKeys.js';



  const rooms = crudFactory.useLocation();
  const headers = useExcludeKeys(rooms.items, ['id']);

  const removeRoom = () => {

  }


  onMounted(async () => {
    await rooms.getAll();
  });

</script>

<template>
  <TableComp :data="rooms.items" :headers="headers">
    <template #extraHeaders>
      <th></th>
    </template>
    <template #extraColumns="{ entity }">
      <td>
        <button
          class="hover:cursor-pointer hover:text-white border p-2 rounded-full"
          @click="removeRoom(entity)"
        >
          Delete
        </button>
      </td>
    </template>
  </TableComp>
</template>

<style scoped>

</style>