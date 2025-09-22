<script setup>
  import { crudFactory } from '@/Services/crudFactory.js';
  import { onMounted, reactive, ref, toRaw, watch } from 'vue';
  import { useJwtClaims } from '@/composable/useJwtClaims.js';
  import { useAccessControl } from '@/composable/useAccsessControl.js';
  import { useExcludeKeys } from '@/composable/useExcludeKeys.js';
  import ProfileOptions from '@/components/UserPage/ProfileOptions.vue';
  import ShowData from '@/components/UserPage/ShowData.vue';
  import EditData from '@/components/UserPage/EditData.vue';
  import Rooms from '@/components/Rooms/Rooms.vue';
  import CustomButton from '@/components/CustomDefaultElements/CustomButton.vue';

  const { parseJwt } = useJwtClaims();
  const { getToken, logout } = useAccessControl();
  const user = crudFactory.useUser();

  const token = getToken();
  const parsedToken = parseJwt(token);

  const filteredData = useExcludeKeys(user.item, ['id']);

  let editing = ref({
    editProfile: false,
    editRooms: false
  });
  const labels = {
    editProfile: 'Edit Profile',
    editRooms: 'Edit Rooms',
  };

  // const editing = ref(false);
  const tempItem = reactive({});
  // const roomsEdit = ref(false);

watch(editing, (newValue, oldValue) => {
  console.log("editing values: ", newValue);
})

  const update = async (key) => {
    if(key === 'editProfile') {
      Object.assign(user.item.value, tempItem);
      const toSend = {
        userName: user.item.value.userName,
        email: user.item.value.email,
      }
      await user.updateItem(parsedToken.id, toSend);
    } else {
      console.log("NO UPDATE LOGIC RUN");
    }
  }

  const deleteProfile = async () => {
    await user.deleteItem(parsedToken.id);
    await logout();
  }

  const editRooms = () => {
    // editing.roomsEdit.value = true;
  }

  onMounted(async () => {
    await user.getSingle(parsedToken.id);

    Object.assign(tempItem, toRaw(user.item.value));
  });

</script>



<template>
  <h1>USER VIEW HERE</h1>
  <div class="w-full flex flex-row justify-between">
    <div class="w-full">
    <ProfileOptions v-model="editing"
                    :labels="labels"
                    @confirm="update"
                    @delete="deleteProfile"
                    @editRooms="editRooms"
    >
      <template #customActions="{keyName, editing}">
        <CustomButton v-if="keyName === 'editProfile'"
                      @click="deleteProfile">
          Delete
        </CustomButton>
      </template>
    </ProfileOptions>
    </div>


    <div class="w-full flex flex-col border-2">
      <section class="w-full p-3">
        <div v-for="(value, key) in filteredData"
             :key="key"
             class="flex flex-row justify-between">
          <h2>{{ key }}:</h2>


          <EditData
            v-if="editing.editProfile === true"
            v-model="tempItem[key]">
          </EditData>

          <ShowData v-else :value="value"></ShowData>


        </div>
      </section>

    </div>
  </div>
  <section v-if="editing.editRooms === true">
    <Rooms/>
  </section>
</template>

<style scoped></style>
