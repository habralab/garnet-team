import { useQuery }          from '@apollo/client'

import { Project }           from '@shared/data'
import { User }              from '@shared/data'
import { mockTeams }         from '@shared/data'

import { GET_JOIN_REQUESTS } from './get-join-requests.query'

interface JoinRequest {
  id?: string
  teamId?: string
  teamName?: string
  projectId?: string
}

export interface MockJoinRequest extends JoinRequest {
  teamDescription?: string
  teamAvatarUrl?: string
  teamProjectCount?: number
  teamParticipants?: User[]
}

const mockJoinRequests: MockJoinRequest[] = mockTeams.map(
  (item, index): MockJoinRequest => ({
    id: String(index),
    teamId: item.id,
    teamName: item.name,
    projectId: String(index),
    teamDescription: item.description,
    teamAvatarUrl: item.avatarUrl,
    teamProjectCount: item.projectCount,
    teamParticipants: item.teamParticipants || [],
  })
)

export interface GetJoinRequestsResponse {
  projectTeamJoinRequest: JoinRequest[]
}

export interface GetJoinRequestsInput {
  id: Project['id']
}

export const useGetJoinRequests = (props: GetJoinRequestsInput) => {
  const { refetch } = useQuery<GetJoinRequestsResponse, GetJoinRequestsInput>(
    GET_JOIN_REQUESTS,
    { variables: props }
  )

  return {
    joinRequests: mockJoinRequests,
    refetch,
  }

  // return {
  //   joinRequests: data?.projectTeamJoinRequest || [],
  //   refetch,
  // }
}
