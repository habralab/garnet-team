import { useMutation }              from '@apollo/client'

import { JoinRequest }              from '@shared/data'

import { CANCEL_TEAM_JOIN_REQUEST } from './cancel-team-join-request.mutation'

export interface CancelTeamJoinRequestResponse {
  teamUserJoinRequestCancel: JoinRequest
}

export interface CancelTeamJoinRequestInput {
  id: JoinRequest['id']
}

export const useCancelTeamJoinRequest = () => {
  const [cancelTeamJoinRequest, { data, loading }] = useMutation<
    CancelTeamJoinRequestResponse,
    CancelTeamJoinRequestInput
  >(CANCEL_TEAM_JOIN_REQUEST)

  return { cancelTeamJoinRequest, data, loading }
}
