import { Team }       from '@shared/data'
import { isIncludes } from '@shared/helpers'

export const filterTeams = (teams: Team[], selectedTags: string[], search: string): Team[] =>
  teams.filter(
    (team) =>
      selectedTags.some((tag) => team.tags?.includes(tag)) ||
      (search.length > 2 && isIncludes(team.name, search))
  )
