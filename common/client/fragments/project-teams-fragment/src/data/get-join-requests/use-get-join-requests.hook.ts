import { useQuery }          from '@apollo/client'

import { Project }           from '@shared/data'

import { GET_JOIN_REQUESTS } from './get-join-requests.query'

export interface JoinRequest {
  id?: string
  projectId?: string
  teamId?: string
  teamName?: string
  teamDescription?: string
  teamAvatarUrl?: string
  projectCount?: number
  teamUserParticipants?: number
}

export interface GetJoinRequestsResponse {
  projectTeamJoinRequest: JoinRequest[]
}

export interface GetJoinRequestsInput {
  id: Project['id']
}

export const useGetJoinRequests = (props: GetJoinRequestsInput) => {
  const { data, refetch } = useQuery<GetJoinRequestsResponse, GetJoinRequestsInput>(
    GET_JOIN_REQUESTS,
    { variables: props }
  )

  return {
    joinRequests: data?.projectTeamJoinRequest || [],
    refetch,
  }
}
