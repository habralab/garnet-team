import { useQuery }        from '@apollo/client'

import { FilterVariables } from '@shared/data'
import { User }            from '@shared/data'

import { GET_USERS }       from './get-users.query'

export interface GetUsersResponse {
  usersFilter: {
    users: User[]
  }
}

export const useGetUsers = (variables: FilterVariables) => {
  const { data, refetch } = useQuery<GetUsersResponse, FilterVariables>(GET_USERS, {
    variables,
  })

  return { users: data?.usersFilter.users || [], refetch }
}
