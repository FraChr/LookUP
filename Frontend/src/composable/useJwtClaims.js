export function useJwtClaims() {
  const parseJwt = (token) => {
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));

      return {
        id: payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'],
        name: payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'],
        exp: payload.exp,
      };
    } catch (e) {
      return null;
    }
  };

  const tokenExpiry = (token) => {
    const payload = parseJwt(token);
    if (!payload || !payload.exp) return null;
    return new Date(payload.exp * 1000);
  };
  return { tokenExpiry };
}
