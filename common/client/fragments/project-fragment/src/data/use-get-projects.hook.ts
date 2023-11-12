import { useQuery }        from '@apollo/client'

import { FilterVariables } from '@shared/data'
import { Project }         from '@shared/data'

import { GET_PROJECTS }    from './get-projects.query'

export interface GetProjectsResponse {
  projectsFilter: {
    projects: Project[]
  }
}

export const useGetProjects = (variables: FilterVariables) => {
  const { data, refetch } = useQuery<GetProjectsResponse, FilterVariables>(GET_PROJECTS, {
    variables,
  })

  return { projects: data?.projectsFilter.projects || [], refetch }
}
