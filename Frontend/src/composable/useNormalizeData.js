export function useNormalizeData() {
  const numberInput = (value) => {
    const regex = /^[0-9]*$/;

    if (!regex.test(value)) {
      return value.replace(/[^0-9]/g, '');
    }
    return value;
  };

  const toUpperCase = (value) => {
    return value.charAt(0).toUpperCase() + value.slice(1);
  };

  const dateFormat = (value) => {
    if (!value || typeof value !== 'string') {
      return 'Invalid Date';
    }

    let date = new Date(value + 'T00:00:00Z');

    return date.toLocaleString(undefined, {
      year: '2-digit',
      month: 'short',
      day: '2-digit',
    });
  };

  return {
    numberInput,
    toUpperCase,
    dateFormat,
  };
}
