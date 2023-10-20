export type CheckPassword = (password: string, repeatPassword: string) => boolean

export const checkPassword: CheckPassword = (password, repeatPassword) => {
  if (password === '' || repeatPassword === '') return false
  return password === repeatPassword
}
