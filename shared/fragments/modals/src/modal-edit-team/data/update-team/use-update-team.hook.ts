import { useMutation } from '@apollo/client'

import { Team }        from '@shared/data'

import { UPDATE_TEAM } from './update-team.mutation'

export interface UpdateTeamResponse {
  TeamEditName: Team
  TeamEditDescription: Team
  TeamEditTags: Team
}

export interface UpdateTeamInput {
  id: Team['id']
  name: Team['name']
  description: Team['description']
  tags: Team['tags']
}

export const useUpdateTeam = () => {
  const [updateTeam, { data, loading }] = useMutation<UpdateTeamResponse, UpdateTeamInput>(
    UPDATE_TEAM
  )

  return { updateTeam, data, loading }
}
