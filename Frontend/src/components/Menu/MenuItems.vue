<script setup>
  const logout = () => {
    console.log(`using logout()`);
    if (localStorage.getItem('token') === null) {
      return;
    }
    localStorage.removeItem('token');
    window.location.reload();
  }
</script>

<template>
  <div>
    <nav class="z-50 absolute h-full w-1/3 left-0 bg-gray-600">
      <ul class="p-5 pl-2 pr-2">
        <li class="mb-1"><RouterLink to="/">Home</RouterLink></li>
        <li class="mb-1"><RouterLink to="/storage">Storage</RouterLink></li>
        <li class="mb-1"><RouterLink to="/user">UserPage</RouterLink></li>
        <li>
           <button @click="logout" class="hover:bg-white bg-amber-200 border-2 px-6 py-2">
             Log Out
           </button>
        </li>
      </ul>
    </nav>
  </div>
</template>

<style scoped></style>
