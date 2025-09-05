<script setup>
import AddItemForm from '@/components/AddItemForm.vue';
import SignUpFrom from '@/components/SignUp/SignUpFrom.vue';
import Login from '@/components/Login/Login.vue';
</script>

<template>
  <div class="border-2 border-amber-500 w-full flex justify-center">
    <SignUpFrom />
  </div>

  <div>
    <Login />
  </div>

  <div class="w-full border-2 border-amber-600 flex justify-center">
<!--    <AddItemForm />-->
  </div>
</template>

<style></style>
