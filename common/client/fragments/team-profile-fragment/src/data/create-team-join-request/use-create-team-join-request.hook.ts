import { useMutation }              from '@apollo/client'

import { JoinRequest }              from '@shared/data'
import { Team }                     from '@shared/data'

import { CREATE_TEAM_JOIN_REQUEST } from './create-team-join-request.mutation'

export interface CreateTeamJoinRequestResponse {
  teamUserJoinRequestCreate: {
    teamUserJoinRequestPayload: JoinRequest
  }
}

export interface CreateTeamJoinRequestInput {
  id: Team['id']
}

export const useCreateTeamJoinRequest = () => {
  const [createTeamJoinRequest, { data, loading }] = useMutation<
    CreateTeamJoinRequestResponse,
    CreateTeamJoinRequestInput
  >(CREATE_TEAM_JOIN_REQUEST)

  return { createTeamJoinRequest, data, loading }
}
