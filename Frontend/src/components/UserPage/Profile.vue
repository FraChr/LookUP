<script setup>
import { crudFactory } from '@/Services/crudFactory.js';
import { onMounted, reactive, ref, toRaw } from 'vue';
import { useJwtClaims } from '@/composable/useJwtClaims.js';
import { useAccessControl } from '@/composable/useAccsessControl.js';
import { useExcludeKeys } from '@/composable/useExcludeKeys.js';
import ShowData from '@/components/UserPage/ShowData.vue';
import CustomButton from '@/components/CustomDefaultElements/CustomButton.vue';
import ProfileOptions from '@/components/ProfileOptions.vue';
import CustomInput from '@/components/CustomDefaultElements/CustomInput.vue';

const { parseJwt } = useJwtClaims();
const { getToken, logout } = useAccessControl();
const user = crudFactory.useUser();

const token = getToken();
const parsedToken = parseJwt(token);

const filteredData = useExcludeKeys(user.item, ['id']);

let editing = ref({
  editProfile: false,
});
const labels = {
  editProfile: 'Edit Profile',
};

const tempItem = reactive({});

const update = async () => {
  Object.assign(user.item.value, tempItem);
  const toSend = {
    userName: user.item.value.userName,
    email: user.item.value.email,
  };
  await user.updateItem(parsedToken.id, toSend);
};

const deleteProfile = async () => {
  await user.deleteItem(parsedToken.id);
  await logout();
};

onMounted(async () => {
  await user.getSingle(parsedToken.id);

  Object.assign(tempItem, toRaw(user.item.value));
  console.log('editing: ', editing);
});
</script>

<template>
  <h1>USER VIEW HERE</h1>
  <div class="grid grid-cols-2 gap-4 w-full">
    <div>
      <ProfileOptions v-model="editing" :labels="labels" @confirm="update">
        <template #customActions="{ keyName, editing }">
          <CustomButton v-if="keyName === 'editProfile'" @click="deleteProfile">
            Delete
          </CustomButton>
        </template>
      </ProfileOptions>
    </div>

    <div class="flex flex-col border-2">
      <div  v-for="(value, key) in filteredData" :key="key" class="m-2 flex flex-row justify-between">
        <h2>{{ key }}:</h2>

        <CustomInput v-if="editing.editProfile === true" v-model="tempItem[key]"> </CustomInput>

        <ShowData class="mt-1" v-else :value="value"></ShowData>
      </div>
    </div>
  </div>
</template>

<style scoped></style>
