<script setup>
import { computed, unref } from 'vue';

  const props = defineProps({
    headers: {
      type: Array,
      required: true
    },
    data: {
      type: [Array, Object],
      required: true
    }
  });
  const resolvedData = computed(() => unref(props.data));
</script>

<template>
  <table class="w-full text-sm text-left rtl:text-right text-primary">
    <thead class="text-xs uppercase bg-contrast text-primary">
      <tr>
        <th v-for="(header, i) in headers"
            :key="`${header}${i}`"
            class="px-6 py-4">
          {{ header }}
        </th>
        <slot name="extraHeaders"></slot>
      </tr>
    </thead>
    <tbody class="[&>*:nth-child(odd)]:bg-extra-1 ">
      <tr v-for="entity in resolvedData"
          :key="`entity-${entity.id}`"
          class="border-b bg-contrast  border-gray-700 group"
          >
        <td v-for="(header, i) in headers"
            :key="`${header}-${i}`"
            class="px-6 py-4 cursor-pointer group-hover:bg-extra-2 group-hover:text-text first:group-hover:rounded-l-2xl"
            :class="[i === headers.length - 1 ? 'group-hover:rounded-r-2xl' : '']"
            @click="$emit('row-click', entity)">
            <slot :name="`column${i}`" :entity="entity">{{entity[header]}}</slot>
        </td>
        <slot name="extraColumns" :entity="entity"></slot>
      </tr>
    </tbody>
  </table>
</template>

<style scoped></style>