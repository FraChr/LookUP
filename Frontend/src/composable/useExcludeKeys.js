import { computed } from 'vue';

export function useExcludeKeys(source, keysToExclude) {
  const excludeSet = new Set(keysToExclude);

  return computed(() => {
    if(Array.isArray(source.value)) {
      const firstItem = source.value[0];
      return firstItem ? Object.keys(firstItem).filter(key => !excludeSet.has(key)) : [];
    }
    else {
      return Object.fromEntries(
        Object.entries(source.value).filter(([key]) => !excludeSet.has(key))
      )
    }
});

  //const headers = computed(() => {
//   const firstItem = storage.items.value[0];
//   return firstItem ? Object.keys(firstItem).filter(key => key !== 'id' && key !== 'locationId') : [];
// });

  // return Object.fromEntries(
  //   Object.entries(source).filter(([key]) => !excludeSet.has(key))
  // )
}