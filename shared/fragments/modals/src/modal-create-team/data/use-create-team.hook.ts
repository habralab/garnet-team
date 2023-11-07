import { useMutation } from '@apollo/client'

import { Team }        from '@shared/data'

import { CREATE_TEAM } from './create-team.mutation'

export interface CreateTeamResponse {
  teamCreate: Team
}

export interface CreateTeamInput {
  name: Team['name']
  description: Team['description']
  tags: Team['tags']
  avatar: Blob
}

export const useCreateTeam = () => {
  const [createTeam, { data, loading }] = useMutation<CreateTeamResponse, CreateTeamInput>(
    CREATE_TEAM
  )

  return { createTeam, data, loading }
}
