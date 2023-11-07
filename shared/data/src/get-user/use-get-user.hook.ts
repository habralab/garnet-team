import { useQuery }        from '@apollo/client'

import { FilterVariables } from '@shared/data'
import { Team }            from '@shared/data'
import { User }            from '@shared/data'

import { Project }         from '../data.interfaces'
import { GET_USER }        from './get-user.query'

export interface GetUserResponse {
  userGet: User
  teamsListByUser: {
    teams: Team[]
  }
  projectFilterByUserParticipantId: {
    projects: Project[]
  }
}

export interface GetUserInput extends Partial<FilterVariables> {
  id: User['id']
}

export const useGetUser = (variables: GetUserInput) => {
  const { data, refetch } = useQuery<GetUserResponse, GetUserInput>(GET_USER, {
    variables,
  })

  return {
    user: data?.userGet,
    teams: data?.teamsListByUser?.teams || [],
    projects: data?.projectFilterByUserParticipantId?.projects || [],
    refetch,
  }
}
