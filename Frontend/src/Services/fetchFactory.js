import {useCrud} from '@/composable/useCrud.js';

export const fetchFactory = {
  useStorage: () => useCrud('storage'),
  useLocation: () => useCrud('location'),

}