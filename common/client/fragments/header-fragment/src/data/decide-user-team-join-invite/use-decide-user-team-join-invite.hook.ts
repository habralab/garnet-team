import { useMutation }                  from '@apollo/client'

import { JoinRequest }                  from '@shared/data'

import { DECIDE_USER_TEAM_JOIN_INVITE } from './decide-user-team-join-invite.mutation'

export interface DecideUserTeamJoinInviteResponse {
  id?: string
  userId?: string
  teamId?: string
}

export interface DecideUserTeamJoinInviteInput {
  id: JoinRequest['id']
  approve: boolean
}

export const useDecideUserTeamJoinInvite = () => {
  const [decideUserTeamJoinInvite, { data, loading }] = useMutation<
    DecideUserTeamJoinInviteResponse,
    DecideUserTeamJoinInviteInput
  >(DECIDE_USER_TEAM_JOIN_INVITE)

  return { decideUserTeamJoinInvite, data, loading }
}
