import { useMutation }    from '@apollo/client'

import { Project }        from '@shared/data'

import { UPDATE_PROJECT } from './update-project.mutation'

export interface UpdateProjectResponse {
  projectEditName: Project
  projectEditDescription: Project
  projectEditTags: Project
}

export interface UpdateProjectInput {
  id: Project['id']
  name: Project['projectName']
  description: Project['description']
  tags: Project['tags']
}

export const useUpdateProject = () => {
  const [updateProject, { data, loading }] = useMutation<UpdateProjectResponse, UpdateProjectInput>(
    UPDATE_PROJECT
  )

  return { updateProject, data, loading }
}
