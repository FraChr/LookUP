export function usePreventExponential() {
  const preventExponential = (value) => {
    const regex = /^[0-9]*$/;

    if (!regex.test(value)) {
      return value.replace(/[^0-9]/g, '');
    }
    return value;
  };

  return { preventExponential };
}
