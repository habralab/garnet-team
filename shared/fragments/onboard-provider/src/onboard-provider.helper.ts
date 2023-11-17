import { User } from '@shared/data'

export const isProfileNotFilled = (user?: User): boolean => {
  if (!user) return true

  return !user.avatarUrl || !user.description || !user.tags || user.tags.length === 0
}
