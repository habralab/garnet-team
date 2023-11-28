import { Project }                from '@shared/data'
import { FormProjectValues }      from '@shared/forms-fragment'

import { useUpdateProject }       from '../data'
import { useUploadProjectAvatar } from '../data'

export const useSubmit = (project?: Project) => {
  const { updateProject, loading: loadingUpdate } = useUpdateProject()
  const { uploadProjectAvatar, loading: loadingUpload } = useUploadProjectAvatar()

  const submit = async (formValues: FormProjectValues): Promise<Project | undefined> => {
    try {
      const { avatar, description, name, tags } = formValues

      if (project?.avatarUrl !== avatar) {
        const avatarFile = await fetch(avatar).then((r) => r.blob())

        await uploadProjectAvatar({ variables: { id: project?.id, file: avatarFile } })
      }

      await updateProject({ variables: { id: project?.id, name, description, tags } })

      return {
        ...project,
        projectName: name,
        description,
        tags,
        avatarUrl: avatar,
      }
    } catch (error) {
      if (process.env.NODE_ENV !== 'production') throw error
    }

    return undefined
  }

  return { submit, loading: loadingUpdate || loadingUpload }
}
