<script setup>
import AddItemForm from '@/components/Storage/AddItemForm.vue';

import { crudFactory } from '@/Services/crudFactory.js';
import { computed, onMounted } from 'vue';
import { useAccessControl } from '@/composable/useAccsessControl.js';
import { useJwtClaims } from '@/composable/useJwtClaims.js';
import Card from '@/components/Card/Card.vue';
import {cardRoutes} from '@/data/cardRoutes.js';
import { useNormalizeData } from '@/composable/useNormalizeData.js';

const {parseJwt} = useJwtClaims();
const {getToken } = useAccessControl();
const { toUpperCase } = useNormalizeData()
const user = crudFactory.useUser();
const token = getToken();
const parsedToken = parseJwt(token);

const username = computed(() => user.item.value.username || '');
const { hasToken } = useAccessControl();

const activeRoutes = computed(() => hasToken.value ? cardRoutes.loggedIn : cardRoutes.notLoggedIn);

onMounted(async () => {
  if(hasToken.value) {
    await user.getSingle(parsedToken.id);
  }
});
</script>

<template>
  <div class="flex justify-center">
    <h1 class="font-bold">{{ hasToken ? `Welcome ${toUpperCase(username)}` : 'Welcome to homepage' }}</h1>
  </div>

  <div class="grid grid-cols-4 gap-4 m-5 max-w-lg mx-auto" >
    <div v-for="(value, key) in activeRoutes" :key="key">
      <RouterLink class="rounded-2xl focus:ring focus:outline-text" :to="value.path">
        <Card :value="value.title"></Card>
      </RouterLink>
    </div>
  </div>

  <div v-if="hasToken" class="w-full flex justify-center">
    <AddItemForm />
  </div>


</template>

<style></style>
