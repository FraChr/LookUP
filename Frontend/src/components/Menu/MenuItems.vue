<script setup>
import { useRouter } from 'vue-router';
import { useAccessControl } from '@/composable/useAccsessControl.js';
import { logout } from '@/Services/tokenHandler.js';
import CustomButton from '@/components/CustomDefaultElements/CustomButton.vue';

const { hasToken } = useAccessControl();
const router = useRouter();
</script>

<template>
  <div>
    <nav class="z-50 bg-contrast text-primary absolute h-full w-1/3 left-0 bg-customGreen">
      <ul class="p-5 pl-2 pr-2 [&>li>*:hover]:text-extra-2
          [&>li>*:hover]:underline text-xl flex flex-col gap-2">
        <li><RouterLink to="/">Home</RouterLink></li>
        <li><RouterLink to="/storage">Storage</RouterLink></li>
        <li><RouterLink to="/user">Profile</RouterLink></li>
        <li>
          <CustomButton
            class="hover:bg-primary border-primary hover:text-contrast"
            v-if="!hasToken"
            @click="router.push('/signup')"

          >
            Signup
          </CustomButton>
        </li>
        <li>
          <CustomButton
            class="hover:bg-primary border-primary hover:text-contrast"
            v-if="!hasToken"
            @click="router.push('/login')"

          >
            Log in
          </CustomButton>
        </li>
        <li>
          <CustomButton :style="'cursor-pointer border-2 border-contrast p-2 rounded-2xl hover:bg-primary border-primary hover:text-contrast'"
                        v-if="hasToken"
                        @click="logout" >
            Log Out
          </CustomButton>
        </li>
      </ul>
    </nav>
  </div>
</template>

<style scoped></style>
