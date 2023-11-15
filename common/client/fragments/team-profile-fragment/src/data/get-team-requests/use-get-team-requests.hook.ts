import { useQuery }          from '@apollo/client'

import { Team }              from '@shared/data'
import { JoinRequest }       from '@shared/data'
import { UserWithRequest }   from '@shared/data'
import { mockUser }          from '@shared/data'

import { GET_TEAM_REQUESTS } from './get-team-requests.query'

export interface GetTeamRequestsResponse {
  teamUserJoinRequestsShow: {
    teamUserJoinRequests: JoinRequest[]
  }
  teamJoinInvitationsShow: {
    teamJoinInvites: JoinRequest[]
  }
}

export interface GetTeamRequestsInput {
  id: Team['id']
}

export const useGetTeamRequests = (props: GetTeamRequestsInput) => {
  const { data, refetch } = useQuery<GetTeamRequestsResponse, GetTeamRequestsInput>(
    GET_TEAM_REQUESTS,
    { variables: props }
  )

  const applicationParticipants: UserWithRequest[] =
    data?.teamUserJoinRequestsShow.teamUserJoinRequests.map((item) => ({
      ...mockUser.userGet,
      requestType: 'application',
      id: item.userId,
      requestId: item.id,
      date: item.createdAt,
    })) || []

  const invitedParticipants: UserWithRequest[] =
    data?.teamJoinInvitationsShow.teamJoinInvites.map((item) => ({
      ...mockUser.userGet,
      requestType: 'invite',
      id: item.userId,
      requestId: item.id,
      date: item.createdAt,
    })) || []

  return {
    applicationParticipants,
    invitedParticipants,
    joinRequests: data?.teamUserJoinRequestsShow.teamUserJoinRequests || [],
    refetch,
  }
}
