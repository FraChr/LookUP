<script setup>
  const props = defineProps({
    headers: {
      type: Array,
      required: true
    },
    data: {
      type: Array,
      required: true
    }
  });
</script>

<template>
  <table class="w-full text-sm text-left rtl:text-right text-gray-400">
    <thead class="text-xs uppercase bg-gray-700 text-gray-400">
      <tr>
        <th v-for="(header, i) in headers"
            :key="`${header}${i}`"
            class="px-6 py-4">{{header}}</th>
        <slot name="extraHeaders"></slot>
      </tr>
    </thead>
    <tbody>
      <tr v-for="entity in data"
          :key="`entity-${entity.name}`"
          class="border-b bg-gray-800 border-gray-700 group"
          @click="$emit('row-click', entity)">
        <td v-for="(header, i) in headers"
            :key="`${header}-${i}`"
            class="px-6 py-4 cursor-pointer group-hover:bg-gray-700 first:group-hover:rounded-l-2xl"
            :class="[i === headers.length - 1 ? 'group-hover:rounded-r-2xl' : '']">
            <slot :name="`column${i}`" :entity="entity"></slot>
        </td>
        <slot name="extraColumns" :entity="entity"></slot>
      </tr>
    </tbody>
  </table>
</template>

<style scoped>

</style>