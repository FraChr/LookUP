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

const { hasToken } = useAccessControl();

const activeRoutes = computed(() => hasToken.value ? cardRoutes.loggedIn : cardRoutes.notLoggedIn);

onMounted(async () => {
  if(hasToken.value) {
    await user.getSingle(parsedToken.id);
  }
})
</script>

<template>
  <div class="flex justify-center">
    <h1 class="font-bold">{{ hasToken ? `Welcome ${user.item.value.username}` : 'Welcome to homepage' }}</h1>
  </div>

  <div v-if="hasToken" class="w-full border-2 border-amber-600 flex justify-center">
    <AddItemForm />
  </div>

  <div  class="grid grid-cols-4 gap 4" >
    <div v-for="(value, key) in activeRoutes" :key="key">
      <RouterLink :to="value.path">
        <Card :value="value.title"></Card>
      </RouterLink>
    </div>
  </div>
</template>

<style></style>
