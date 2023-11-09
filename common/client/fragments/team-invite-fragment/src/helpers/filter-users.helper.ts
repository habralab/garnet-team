import { User }       from '@shared/data'
import { isIncludes } from '@shared/helpers'

export const filterUsers = (users: User[], selectedTags: string[], search: string): User[] =>
  users.filter(
    (user) =>
      selectedTags.some((tag) => user.tags?.includes(tag)) ||
      (search.length > 2 && isIncludes(user.userName, search))
  )
