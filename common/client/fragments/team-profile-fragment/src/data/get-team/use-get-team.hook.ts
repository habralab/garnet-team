import { useQuery }        from '@apollo/client'

import { FilterVariables } from '@shared/data'
import { Team }            from '@shared/data'
import { Project }         from '@shared/data'
import { User }            from '@shared/data'
import { mockUser }        from '@shared/data'

import { GET_TEAM }        from './get-team.query'

export interface GetTeamResponse {
  teamGet: Team
  teamParticipantFilter: {
    teamParticipants: {
      id?: string
      userId?: string
      teamId?: string
    }[]
  }
  projectFilterByTeamParticipantId: {
    projects: Project[]
  }
}

export interface GetTeamInput extends Partial<FilterVariables> {
  id: Team['id']
}

export const useGetTeam = ({ id, search = '', skip = 0, take = 0 }: GetTeamInput) => {
  const { data, refetch } = useQuery<GetTeamResponse, GetTeamInput>(GET_TEAM, {
    variables: { id, search, skip, take },
  })

  const teamParticipants: User[] =
    data?.teamParticipantFilter.teamParticipants.map((item) => ({
      ...mockUser.userGet,
      id: item.userId,
    })) || []

  return {
    team: data?.teamGet,
    teamProjects: data?.projectFilterByTeamParticipantId.projects || [],
    teamParticipants,
    refetch,
  }
}
