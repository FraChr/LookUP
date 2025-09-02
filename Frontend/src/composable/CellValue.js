export function useCellValue() {
  const getCellValue = (item, header) => {
    if(header === 'location') return item.location?.name || '';
    return item[header];
  }
  return {getCellValue};
}