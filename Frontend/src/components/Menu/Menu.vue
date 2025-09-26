<script setup>
import { onBeforeUnmount, onMounted, ref, watch } from 'vue';
import MenuItems from '@/components/Menu/MenuItems.vue';
import { useRoute } from 'vue-router';


const menu = ref(null);
const isMenuOpen = ref(false);
const route = useRoute();

watch(route, () => {
  isMenuOpen.value = false;
});

function handleClickOutside(event) {
  if(menu.value && !menu.value.contains(event.target)) {
    isMenuOpen.value = false;
  }
}

const toggleMenu = () => {
  isMenuOpen.value = !isMenuOpen.value;
};

onMounted(() => {
  document.addEventListener('click', handleClickOutside);
});
onBeforeUnmount(() => {
  document.removeEventListener('click', handleClickOutside);
})
</script>

<template>
  <div ref="menu">
    <button
      @click="toggleMenu"
      class="cursor-pointer relative w-8 h-8 flex flex-col justify-around items-center"
    >
      <span class="w-6 h-0.5 bg-primary"></span>
      <span class="w-6 h-0.5 bg-primary"></span>
      <span class="w-6 h-0.5 bg-primary"></span>
    </button>

    <MenuItems v-if="isMenuOpen" />
  </div>
</template>

<style scoped></style>
