<script setup>
  import CustomButton from '@/components/CustomDefaultElements/CustomButton.vue';
  import { crudFactory } from '@/Services/crudFactory.js';
  import { onMounted, ref, watch } from 'vue';
  import { useJwtClaims } from '@/composable/useJwtClaims.js';
  import { useAccessControl } from '@/composable/useAccsessControl.js';
  import { useExcludeKeys } from '@/composable/useExcludeKeys.js';
  import ProfileOptions from '@/components/UserPage/ProfileOptions.vue';
  import CustomInput from '@/components/CustomDefaultElements/CustomInput.vue';
  import ShowData from '@/components/UserPage/ShowData.vue';
  import EditData from '@/components/UserPage/EditData.vue';

  const { parseJwt } = useJwtClaims();
  const { getToken, logout } = useAccessControl();
  const user = crudFactory.useUser();

  const token = getToken();
  const parsedToken = parseJwt(token);

  const filteredData = useExcludeKeys(user.item, ['id']);
  const editing = ref(false);
  const tempItem = ref();

  // async function test() {
  //   console.log("Delete clicked!!!");
  //   // await user.deleteItem(parsedToken.id);
  //   // await logout();
  // }
  //
  // function editStorage() {
  //   console.log("Edit Storage Clicked!!!");
  // }
  // function editProfile() {
  //   console.log("edit Profile Clicked!!!");
  // }

  watch(editing, newVal => {
    console.log("editing value", newVal)
  });
  watch(tempItem, newVal => {
    console.log("tempItem", newVal);
  });

  const t = () => {
    console.log("editing user with func t");
    Object.assign(tempItem.value, user.item.value);
  }

  const confirmEdit = () => {
    // user.item.value[key] = tempItem[key];
    Object.assign(user.item.value, tempItem.value);
    update();
  }

  const update = async () => {

    const toSend = {
      userName: user.userName,
      email: user.email,
      userId: user.id,
    }
    await user.updateItem(parsedToken.id, toSend);
  }

  onMounted(() => {
    user.getSingle(parsedToken.id);
  });

</script>



<template>
  <h1>USER VIEW HERE</h1>
  <div class="w-full flex flex-row justify-between">
    <ProfileOptions v-model="editing"
                    @editing="val => editing = val"
                    @confirm="confirmEdit"
                    @startEdit="t"
    ></ProfileOptions>
  <div class="w-full flex border-2">
    <section class="w-full p-3">
      <div v-for="(value, key) in filteredData"
           :key="key"
           class="flex flex-row justify-between">
        <h2>{{ key }}:</h2>


        <EditData
          v-if="editing === true"
          v-model="tempItem[key]"
          :keyName="key"
          @editing="val => editing = val">
        </EditData>
<!--        <template v-if="editing === true">-->
<!--          <CustomInput />-->
<!--        </template>-->

        <ShowData v-else
                  :value="value"></ShowData>
<!--        <template v-else>-->
<!--          <p>{{ value }}</p>-->
<!--        </template>-->
      </div>
    </section>
  </div>
  </div>
</template>

<style scoped></style>
