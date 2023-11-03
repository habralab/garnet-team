export const getPathAndText = (url: string) => {
  switch (url) {
    case '/auth/login':
      return {
        redirect: '/auth/registration',
        text: 'login_password.header',
      }
    case '/auth/registration':
      return {
        redirect: '/auth/login',
        text: 'registration_password.header',
      }
    default:
      return {
        redirect: '/auth/login',
        text: 'registration_password.header',
      }
  }
}
