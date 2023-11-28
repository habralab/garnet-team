import { Team }                from '@shared/data'
import { FormTeamValues }      from '@shared/forms-fragment'

import { useUpdateTeam }       from '../data'
import { useUploadTeamAvatar } from '../data'

export const useSubmit = (team?: Team) => {
  const { updateTeam, loading: loadingUpdate } = useUpdateTeam()
  const { uploadTeamAvatar, loading: loadingUpload } = useUploadTeamAvatar()

  const submit = async (formValues: FormTeamValues): Promise<Team | undefined> => {
    try {
      const { avatar, description, name, tags } = formValues

      if (team?.avatarUrl !== avatar) {
        const avatarFile = await fetch(avatar).then((r) => r.blob())

        await uploadTeamAvatar({ variables: { id: team?.id, file: avatarFile } })
      }

      await updateTeam({ variables: { id: team?.id, name, description, tags } })

      return {
        ...team,
        name,
        description,
        tags,
        avatarUrl: avatar,
      }
    } catch (error) {
      if (process.env.NODE_ENV !== 'production') throw error
    }
  }

  return { submit, loading: loadingUpdate || loadingUpload }
}
