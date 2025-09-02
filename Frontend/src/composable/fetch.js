import { getStorage } from '@/Services/api.js';
import { computed, ref } from 'vue';

export function useFetch() {
  const items = ref([]);
  const total = ref(0);
  const currentPage = ref(1);
  const pageSize = ref(10);
  const totalPages = computed(() => Math.ceil(total.value / pageSize.value));

  const fetchItems = async (searchTerm = null) => {
    try {
      const response = await getStorage({
        params: {
          limit: pageSize.value,
          page: currentPage.value,
          searchTerm: searchTerm,
        },
      });
      items.value = response.data.data;
      total.value = response.data.total;
      console.log(items.value)
    } catch (error) {
      console.error(`Error fetching data ${error}`);
    }
  };

  return {
    items,
    total,
    currentPage,
    pageSize,
    totalPages,
    fetchItems,
  };
}
