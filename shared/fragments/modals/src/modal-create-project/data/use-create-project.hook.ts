import { useMutation }    from '@apollo/client'

import { Project }        from '@shared/data'

import { CREATE_PROJECT } from './create-project.mutation'

export interface CreateProjectResponse {
  projectCreate: Project
}

export interface CreateProjectInput {
  name: Project['projectName']
  description: Project['description']
  tags: Project['tags']
  avatar: Blob
}

export const useCreateProject = () => {
  const [createProject, { data, loading }] = useMutation<CreateProjectResponse, CreateProjectInput>(
    CREATE_PROJECT
  )

  return { createProject, data, loading }
}
