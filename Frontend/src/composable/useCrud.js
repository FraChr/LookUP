import { createCrudService } from '@/Services/api.js';
import { computed, ref } from 'vue';

export function useCrud(route) {
  const service = createCrudService(route);

  const items = ref([]);
  const item = ref({});
  const total = ref(0);
  const currentPage = ref(1);
  const pageSize = ref(10);
  const totalPages = computed(() => Math.ceil(total.value / pageSize.value));
  // const error = ref('');

  const getAll = async (searchTerm = null) => {
    try {
      const response = await service.list({
        params: {
          limit: pageSize.value,
          page: currentPage.value,
          searchTerm: searchTerm,
        },
      });

      console.log("response", response.data)
      items.value = response.data.data;

      total.value = response.data.total;
      console.log(items.value)
    } catch (error) {
      console.error(`Error fetching data ${error.response.data}`);
    }
  };

  const getSingle = async (id) => {
    try {
      const response = await service.getById(id);
      console.log("response FROM GetSingle: ", response.data);
      item.value = response.data
    } catch (e) {
      console.error('Error fetching item', e);
    }
  }

  const updateItem = async (id, data) => {
    try {
      const response = await service.update(id, data);
      item.value = response.data;
    } catch (error) {
      // console.error('Error updating item', error.message, '|', error.detail);
    }
  }
  const addItem = async (data) => {
    try {

      const response = await service.create(data);

    } catch (error) {
      console.error('error response: ', error.response.data);
      // errorMsg.value = error.response?.data?.message || error.message;
    }
  };

  const deleteItem = async (id) => {
    try {
      await service.delete(id);

    } catch (error) {
      console.error('Error deleting item: ', error);
    }
  }


  return {
    items,
    item,
    total,
    currentPage,
    pageSize,
    totalPages,
    getAll,
    getSingle,
    updateItem,
    addItem,
    deleteItem,
  };
}
