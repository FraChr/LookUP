<script setup>
// import { addItem } from '@/Services/api.js';
import { onMounted, ref, watch } from 'vue';

import { useNormalizeData } from '@/composable/useNormalizeData.js';

import { crudFactory } from '@/Services/crudFactory.js';
import Select from '@/Select.vue';
import CustomInput from '@/components/CustomDefaultElements/CustomInput.vue';
import CustomButton from '@/components/CustomDefaultElements/CustomButton.vue';
import EditOptions from '@/components/EditOptions.vue';
import ProfileLayout from '@/components/UserPage/ProfileLayout.vue';

const storage = crudFactory.useStorage();
const location = crudFactory.useLocation();
const shelfs = crudFactory.useShelfs();

const selectedRoom = ref('');
const selectedShelf = ref('');
const tag = ref('');
const amount = ref(null);
const errorMessage = ref('');

const { numberInput } = useNormalizeData();

const editing = ref({
  AddStorage: false,
});
const labels = {
  AddStorage: 'Add to Storage',
}


const add = () => {
  errorMessage.value = '';
  try {
    const item = {
      Name: tag.value,
      Amount: Number(amount.value),
      LocationId: Number(selectedRoom.value),
      ShelfsId: Number(selectedShelf.value),
    };

    storage.addItem(item);
    selectedRoom.value = '';
    selectedShelf.value = '';
    tag.value = '';
    amount.value = null
  } catch (error) {
    console.error('error response: ', error.response.data);

    if (error.response && error.response.data.message) {
      errorMessage.value = error.response.data.message;
    } else {
      console.error('unknown error: ', error.message);
    }
  }
};

watch(errorMessage, (newVal) => {
  console.log("error response in watch: ", newVal);
});

onMounted(() => {
  location.getAll();
  shelfs.getAll();
});
</script>

<template>

  <ProfileLayout>
    <template #Right>
      <EditOptions v-model="editing" :labels="labels" @confirm="add">

      </EditOptions>


    </template>

    <template #Left>
      <div class="w-full flex flex-col items-center m-4">
        <form v-if="editing.AddStorage" @submit.prevent="add" class="border-2 border-contrast w-full max-w-md flex flex-col space-y-3 p-6">
          <Select

            v-model="selectedRoom"
            :options="location.items.value"
            :defaultOption="true"
            :defaultOptionValue="'Select Room'"
          />
          <Select
            required
            v-model="selectedShelf"
            :options="shelfs.items.value"
            :defaultOption="true"
            :defaultOptionValue="'Select Shelf'"
          />
          <CustomInput v-model="tag"
                       type="text"
                       placeholder="Item Tag"
                       class="border-2 p-2"
                       required />
          <CustomInput
            v-model="amount"
            @input="amount = numberInput($event.target.value)"
            placeholder="Amount"
            class="border-2 p-2"
          />

<!--          <div class="flex justify-center">-->
<!--            <CustomButton type="submit"-->
<!--                          class="px-6 py-2"-->
<!--              >-->
<!--              Add</CustomButton>-->
<!--          </div>-->
        </form>
        <div v-if="errorMessage" class="text-red-500 font-semibold p-2 text-center">
          {{ errorMessage }}
        </div>
      </div>
    </template>
  </ProfileLayout>


</template>

<style scoped></style>
