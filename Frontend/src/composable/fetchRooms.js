import { getRooms } from '@/Services/api.js';
import { ref } from 'vue';

export function useFetchRooms() {
  const rooms = ref([]);

  const getRoomsData = async () => {
    const response = await getRooms();
    rooms.value = response.data.data;
  };

  return { rooms, getRoomsData };
}
