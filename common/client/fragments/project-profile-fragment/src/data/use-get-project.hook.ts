import { useQuery }    from '@apollo/client'

import { Team }        from '@shared/data'
import { Project }     from '@shared/data'

import { GET_PROJECT } from './get-project.query'

export interface GetProjectResponse {
  projectGet: Project
  projectTeamParticipantsFilter: {
    projectTeamParticipant: Team[]
  }
}

export interface GetProjectInput {
  id: Project['id']
}

export const useGetProject = (props: GetProjectInput) => {
  const { data, refetch } = useQuery<GetProjectResponse, GetProjectInput>(GET_PROJECT, {
    variables: props,
  })

  return {
    project: data?.projectGet,
    projectTeams: data?.projectTeamParticipantsFilter.projectTeamParticipant || [],
    refetch,
  }
}
