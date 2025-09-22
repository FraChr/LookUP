<script setup>
  import CustomButton from '@/components/CustomDefaultElements/CustomButton.vue';
  const props = defineProps({
    modelValue: {
      type: [Object, Array],
      required: true,
    },
    actions: {
      type: Array,
      default: () => ['edit', 'confirm'],
    },
    labels: {}

  });
  const emit = defineEmits(['update:modelValue', 'confirm', 'delete', 'edit']);

  const updateModelValue = (key, bool) => {
    return {
      ...props.modelValue,
      [key]: bool
    };
  }

  function onEdit(key) {
    const updated = updateModelValue(key, true);
    emit('update:modelValue', updated);
    emit('edit', key);
  }

  function onConfirm(key) {
    emit('confirm', key);
    emit('update:modelValue', updateModelValue(key, false));
  }

  function onCancel(key) {
    const updated = updateModelValue(key, false);
    emit('update:modelValue', updated);
  }

  // function onDelete(key) {
  //   emit('delete', key);
  // }


  // async function DeleteProfile() {
  //   console.log("Delete clicked!!!");
  //   emit('delete');
  // }
  //
  // function editStorage() {
  //   console.log("Edit Storage Clicked!!!");
  //   emit('update:modelValue', true)
  //   emit('editRooms');
  // }
  // function editProfile(key) {
  //   console.log("edit Profile Clicked!!!");
  //   // emit('update:modelValue', true);
  //
  //   // const updated = {
  //   //   ...props.modelValue,
  //   //   editProfile: true
  //   // };
  //
  //   let updated = updateModelValue(key, true);
  //
  //   // console.log("Edit Profile Clicked!!!", updated);
  //   emit('update:modelValue', updated);
  //   console.log("Edit Profile Clicked!!!", props.modelValue);
  // }
  // function confirmEditProfile() {
  //   console.log("Confirm Edit Profile Clicked!!!");
  //   emit('confirm');
  // }


  // function cancelEditProfile(key) {
  //   let updated = updateModelValue(key, false)
  //   emit('update:modelValue', updated);
  // }




</script>

<template>

  <div v-for="(value, key) in modelValue" :key="key" class="w-full flex border-2 gap-2 flex-col">
    <h3 class="font-bold">{{ labels[key] ?? key }}</h3>
    <div class="flex flex-row" v-if="value === true">
      <CustomButton class="w-full" @click="onConfirm(key)">Acc</CustomButton>
      <CustomButton class="w-full" @click="onCancel(key)">X</CustomButton>
    </div>

    <template v-else>
      <CustomButton v-if="actions.includes('edit')" @click="onEdit(key)">Edit</CustomButton>
    </template>

    <slot name="customActions" :keyName="key" :editing="value === true"></slot>
  </div>



<!--  <div v-for="(value, key) in modelValue" :key="key" class="w-full flex border-2 gap-2 flex-col">-->
<!--    <template-->
<!--      v-if="value === true"-->
<!--      >-->
<!--      <div class="flex flex-row justify-evenly w-full">-->
<!--        <CustomButton class="w-full" @click="confirmEditProfile">Acc</CustomButton>-->
<!--        <CustomButton class="w-full" @click="cancelEditProfile(key)">X</CustomButton>-->
<!--      </div>-->
<!--    </template>-->
<!--    <template v-else>-->
<!--      <CustomButton @click="editProfile(key)">Edit Profile</CustomButton>-->
<!--    </template>-->
<!--    <CustomButton @click="editStorage">Edit Storage</CustomButton>-->
<!--    <CustomButton @click="DeleteProfile">Delete Account</CustomButton>-->
<!--  </div>-->
</template>

<style scoped>

</style>