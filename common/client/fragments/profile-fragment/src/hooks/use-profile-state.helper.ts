import { IsProfileFilled } from './use-profile-state.interfaces'

export const isProfileNotFilled: IsProfileFilled = (user) => {
  if (!user) return true

  return !user.avatarUrl || !user.description || !user.tags || user.tags.length === 0
}
