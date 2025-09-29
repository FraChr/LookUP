<script setup>
  import Profile from '@/components/UserPage/Profile.vue';
  import Rooms from '@/components/Rooms/Rooms.vue';
  import Shelfs from '@/components/Shelfs/Shelfs.vue';
  import AddItemForm from '@/components/Storage/AddItemForm.vue';
</script>

<template>
  <div class="w-1/2 flex flex-col gap-4">
    <Profile />
    <Rooms />
    <Shelfs />
    <AddItemForm />
  </div>
</template>

<style scoped></style>
