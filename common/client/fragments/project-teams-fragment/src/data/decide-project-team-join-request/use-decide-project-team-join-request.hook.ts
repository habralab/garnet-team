import { useMutation }                      from '@apollo/client'

import { JoinRequest }                      from '@shared/data'

import { DECIDE_PROJECT_TEAM_JOIN_REQUEST } from './decide-project-team-join-request.mutation'

export interface DecideProjectTeamJoinRequestResponse {
  id?: string
  teamId?: string
  teamName?: string
  projectId?: string
}

export interface DecideProjectTeamJoinRequestInput {
  id: JoinRequest['id']
  approve: boolean
}

export const useDecideProjectTeamJoinRequest = () => {
  const [decideProjectTeamJoinRequest, { data, loading }] = useMutation<
    DecideProjectTeamJoinRequestResponse,
    DecideProjectTeamJoinRequestInput
  >(DECIDE_PROJECT_TEAM_JOIN_REQUEST)

  return { decideProjectTeamJoinRequest, data, loading }
}
