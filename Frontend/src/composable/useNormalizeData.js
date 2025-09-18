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
  }

  return {
    numberInput,
    toUpperCase,
  };
}
