import { useMutation }              from '@apollo/client'

import { JoinRequest }              from '@shared/data'

import { DECIDE_TEAM_JOIN_REQUEST } from './decide-team-join-request.mutation'

export interface DecideTeamJoinRequestResponse {
  teamUserJoinRequestDecide: {
    teamUserJoinRequestPayload: JoinRequest
  }
}

export interface DecideTeamJoinRequestInput {
  id: JoinRequest['id']
  approve: boolean
}

export const useDecideTeamJoinRequest = () => {
  const [decideTeamJoinRequest, { data, loading }] = useMutation<
    DecideTeamJoinRequestResponse,
    DecideTeamJoinRequestInput
  >(DECIDE_TEAM_JOIN_REQUEST)

  return { decideTeamJoinRequest, data, loading }
}
