<script setup>
import { crudFactory } from '@/Services/crudFactory.js';
import { onMounted, reactive, ref, toRaw } from 'vue';
import { useJwtClaims } from '@/composable/useJwtClaims.js';
import { useAccessControl } from '@/composable/useAccsessControl.js';
import { useExcludeKeys } from '@/composable/useExcludeKeys.js';
import ShowData from '@/components/UserPage/ShowData.vue';
import CustomButton from '@/components/CustomDefaultElements/CustomButton.vue';
import EditOptions from '@/components/EditOptions.vue';
import CustomInput from '@/components/CustomDefaultElements/CustomInput.vue';
import ConfirmDelete from '@/components/ConfirmDelete.vue';
import Popup from '@/components/Popup.vue';
import ProfileLayout from '@/components/UserPage/ProfileLayout.vue';

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
const showConfirmDelete = ref(false);

const tempItem = reactive({});

const update = async () => {
  Object.assign(user.item.value, tempItem);
  const toSend = {
    username: user.item.value.username,
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
});
</script>

<template>
  <ProfileLayout>
    <template #Right>
      <EditOptions v-model="editing" :labels="labels" @confirm="update">
        <template #customActions="{ keyName, editing }">
          <CustomButton v-if="keyName === 'editProfile'" @click="showConfirmDelete = true">
            Delete
          </CustomButton>
        </template>
      </EditOptions>
    </template>

    <template #Left>

        <div
          v-for="(value, key) in filteredData"
          :key="key"
        >
          <h2>{{ key }}:</h2>

          <CustomInput v-if="editing.editProfile === true" v-model="tempItem[key]"> </CustomInput>

          <ShowData class="mt-1" v-else :value="value"></ShowData>
        </div>
    </template>
  </ProfileLayout>

  <Popup :visible="showConfirmDelete">
    <ConfirmDelete :showConfirmDelete="true"
                   message="Warning: DELETE USER?"
                   @confirmDelete="deleteProfile"
                   @cancelDelete="showConfirmDelete = false"></ConfirmDelete>
  </Popup>
</template>

<style scoped></style>
