import { useQuery }        from '@apollo/client'

import { FilterVariables } from '@shared/data'
import { Team }            from '@shared/data'
import { User }            from '@shared/data'
import { mockMyUser }      from '@shared/data'

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

export const useGetUser = ({ id, search = '', skip = 0, tags = [], take = 0 }: GetUserInput) => {
  const { data, refetch } = useQuery<GetUserResponse, GetUserInput>(GET_USER, {
    variables: { id, search, skip, tags, take },
  })

  if (!data) {
    return {
      user: mockMyUser.userGet,
      teams: mockMyUser?.teamsListByUser?.teams || [],
      projects: mockMyUser?.projectsListByUser?.projects || [],
    }
  }


  return {
    user: data?.userGet,
    teams: data?.teamsListByUser?.teams || [],
    projects: data?.projectFilterByUserParticipantId?.projects || [],
    refetch,
  }
}
