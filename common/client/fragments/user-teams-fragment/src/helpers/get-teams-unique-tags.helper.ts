import { Team } from '@shared/data'

export const getTeamsUniqueTags = (teams: Team[]): string[] => {
  const allTeamsTags = teams.map((team) => team.tags).flat()

  const setUniqueTags = new Set<string | undefined>(allTeamsTags)

  setUniqueTags.delete(undefined)

  return Array.from(setUniqueTags) as string[]
}
