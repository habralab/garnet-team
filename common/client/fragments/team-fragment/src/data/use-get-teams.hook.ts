import { useQuery }        from '@apollo/client'

import { FilterVariables } from '@shared/data'
import { Team }            from '@shared/data'

import { GET_TEAMS }       from './get-teams.query'

export interface GetTeamsResponse {
  teamsFilter: {
    teams: Team[]
  }
}

export const useGetTeams = (variables: FilterVariables) => {
  const { data, refetch } = useQuery<GetTeamsResponse, FilterVariables>(GET_TEAMS, {
    variables,
  })

  return { teams: data?.teamsFilter.teams || [], refetch }
}
