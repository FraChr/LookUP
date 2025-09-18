import { useCrud } from '@/composable/useCrud.js';

export const crudFactory = {
  useStorage: () => useCrud('storage'),
  useLocation: () => useCrud('location'),
  useShelfs: () => useCrud('shelfs'),
};
