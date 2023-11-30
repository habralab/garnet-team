import { useMutation }                      from '@apollo/client'

import { JoinRequest }                      from '@shared/data'

import { CREATE_TEAM_JOIN_PROJECT_REQUEST } from './create-team-join-project-request.mutation'

export interface CreateTeamJoinProjectRequestResponse {
  teamJoinProjectRequest: JoinRequest
}

export interface CreateTeamJoinProjectRequestInput {
  projectId: string
  teamId: string
}

export const useCreateTeamJoinProjectRequest = () => {
  const [createTeamJoinProjectRequest, { data, loading }] = useMutation<
    CreateTeamJoinProjectRequestResponse,
    CreateTeamJoinProjectRequestInput
  >(CREATE_TEAM_JOIN_PROJECT_REQUEST)

  return { createTeamJoinProjectRequest, data, loading }
}
