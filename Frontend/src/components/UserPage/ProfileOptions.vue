<script setup>
  import CustomButton from '@/components/CustomDefaultElements/CustomButton.vue';
  const props = defineProps({
    modelValue: {},

  });
  const emit = defineEmits(['update:modelValue', 'confirm', 'startEdit']);

  async function DeleteProfile() {
    console.log("Delete clicked!!!");
    // await user.deleteItem(parsedToken.id);
    // await logout();
  }

  function editStorage() {
    console.log("Edit Storage Clicked!!!");

  }
  function editProfile() {
    console.log("edit Profile Clicked!!!");
    // emit('editing', true);
    emit('update:modelValue', true);
    emit('startEdit');
  }
  function confirmEditProfile() {
    console.log("Confirm Edit Profile Clicked!!!");
    emit('confirm');
  }
  function cancelEditProfile() {
    emit('update:modelValue', false);
  }

</script>

<template>
  <div class="w-full flex border-2 gap-2 flex-col">
    <template
      v-if="modelValue === true"
      >
      <div class="flex flex-row justify-evenly w-full">
      <CustomButton class="w-full" @click="confirmEditProfile">Acc</CustomButton>
      <CustomButton class="w-full" @click="cancelEditProfile">XX</CustomButton>
      </div>
    </template>
    <template v-else>
      <CustomButton @click="editProfile">Edit Profile</CustomButton>
    </template>
    <CustomButton @click="editStorage">Edit Storage</CustomButton>
    <CustomButton @click="DeleteProfile">Delete Account</CustomButton>
  </div>
</template>

<style scoped>

</style>