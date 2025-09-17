import { computed } from 'vue';

export function useDateFormat(raw) {

  if(!raw || typeof raw !== 'string') {
    return "Invalid Date";
  }

  let date = new Date(raw + 'T00:00:00Z');

  return date.toLocaleString(undefined, {
    year: '2-digit',
    month: 'short',
    day: '2-digit',
  })
}