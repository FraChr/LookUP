import { getRooms } from '@/Services/api.js';
import { ref } from 'vue';
import { fetchFactory } from '@/Services/fetchFactory.js';

const {getAll, items} = fetchFactory.useLocation()

export function useFetchRooms() {
  const rooms = ref(items);

  const getRoomsData = async () => {
    // const response = await getRooms();
    // rooms.value = response.data.data;
    await getAll();
    console.log("FROM USEFETCHROOMS : ", items.value);
  };

  return { rooms, getRoomsData };
}
