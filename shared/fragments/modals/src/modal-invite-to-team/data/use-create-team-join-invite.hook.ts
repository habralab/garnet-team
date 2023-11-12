import { useMutation }             from '@apollo/client'

import { JoinRequest }             from '@shared/data'

import { CREATE_TEAM_JOIN_INVITE } from './create-team-join-invite.mutation'

export interface CreateTeamJoinInviteResponse {
  teamJoinInvite: JoinRequest
}

export interface CreateTeamJoinInviteInput {
  userId: string
  teamId: string
}

export const useCreateTeamJoinInvite = () => {
  const [createTeamJoiInvite, { data, loading }] = useMutation<
    CreateTeamJoinInviteResponse,
    CreateTeamJoinInviteInput
  >(CREATE_TEAM_JOIN_INVITE)

  return { createTeamJoiInvite, data, loading }
}
