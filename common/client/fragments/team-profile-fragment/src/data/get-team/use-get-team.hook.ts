import { useQuery }        from '@apollo/client'

import { FilterVariables } from '@shared/data'
import { Team }            from '@shared/data'
import { JoinRequest }     from '@shared/data'
import { Project }         from '@shared/data'
import { User }            from '@shared/data'
import { UserWithRequest } from '@shared/data'
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
  teamUserJoinRequestsShow: {
    teamUserJoinRequests: JoinRequest[]
  }
  teamJoinInvitationsShow: {
    teamJoinInvites: JoinRequest[]
  }
}

export interface GetTeamInput extends Partial<FilterVariables> {
  id: Team['id']
}

export const useGetTeam = (props: GetTeamInput) => {
  const { data, refetch } = useQuery<GetTeamResponse, GetTeamInput>(GET_TEAM, {
    variables: props,
  })

  const teamParticipants: User[] =
    data?.teamParticipantFilter.teamParticipants.map((item) => ({
      ...mockUser.userGet,
      id: item.userId,
    })) || []

  const applicationParticipants: UserWithRequest[] =
    data?.teamUserJoinRequestsShow.teamUserJoinRequests.map((item) => ({
      ...mockUser.userGet,
      requestType: 'application',
      id: item.userId,
      date: item.createdAt,
    })) || []

  const invitedParticipants: UserWithRequest[] =
    data?.teamJoinInvitationsShow.teamJoinInvites.map((item) => ({
      ...mockUser.userGet,
      requestType: 'invite',
      id: item.userId,
      date: item.createdAt,
    })) || []

  return {
    team: data?.teamGet,
    teamProjects: data?.projectFilterByTeamParticipantId.projects || [],
    teamParticipants,
    applicationParticipants,
    invitedParticipants,
    refetch,
  }
}
