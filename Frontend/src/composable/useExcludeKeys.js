import { computed } from 'vue';

export function useExcludeKeys(source, keysToExclude) {
  const excludeSet = new Set(keysToExclude);

  return computed(() => {
    return Object.fromEntries(
      Object.entries(source.value).filter(([key]) => !excludeSet.has(key))
    )
});

  // return Object.fromEntries(
  //   Object.entries(source).filter(([key]) => !excludeSet.has(key))
  // )
}