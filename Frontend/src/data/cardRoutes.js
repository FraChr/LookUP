export const cardRoutes = {
  notLoggedIn: {
    toLogin: {
      title: "Login",
      path: "/login",
    },
    toSignup: {
      title: "Signup",
      path: "/signup",
    },
  },
  loggedIn: {
    toProfile: {
      title: "Profile",
      path: "/user",
    },
    toStorage: {
      title: "Storage",
      path: "/storage",
    },
  }
};