import { useQuery }           from '@apollo/client'

import { GetProjectInput }    from './get-project.interfaces'
import { GetProjectResponse } from './get-project.interfaces'
import { GET_PROJECT }        from './get-project.query'
import { parseProjectTeams }  from '../helpers'

export const useGetProject = (props: GetProjectInput) => {
  const { data, refetch } = useQuery<GetProjectResponse, GetProjectInput>(GET_PROJECT, {
    variables: props,
  })

  return {
    project: data?.projectGet,
    projectTeams: parseProjectTeams(
      data?.projectTeamParticipantsFilter?.projectTeamParticipant || []
    ),
    refetch,
  }
}
