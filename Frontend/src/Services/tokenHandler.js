let tokenGetter = () => null;
let logoutHandler = () => {};

export const setTokenHandler = (getter) => {
  tokenGetter = getter;
};

export const setLogoutHandler = (handler) => {
  logoutHandler = handler;
};

export const getToken = () => tokenGetter();
export const logout = () => logoutHandler();
