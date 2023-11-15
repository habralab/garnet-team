import { User }                from '@shared/data'
import { FormUserValues }      from '@shared/forms-fragment'

import { useUpdateUser }       from '../data'
import { useUploadUserAvatar } from '../data'

export const useSubmit = (user?: User) => {
  const { updateUser, loading: loadingUpdate } = useUpdateUser()
  const { uploadUserAvatar, loading: loadingUpload } = useUploadUserAvatar()

  const submit = async (formValues: FormUserValues): Promise<User | undefined> => {
    try {
      const { avatar, description, name, surname, tags } = formValues

      if (user?.avatarUrl !== avatar) {
        const avatarFile = await fetch(avatar).then((r) => r.blob())

        await uploadUserAvatar({ variables: { file: avatarFile } })
      }

      const editedUser: Required<User> = {
        id: user?.id || '',
        userName: `${name} ${surname}`,
        description,
        tags,
        avatarUrl: avatar,
      }

      await updateUser({ variables: editedUser })

      return editedUser
    } catch (error) {
      /** @todo error notification */

      return undefined
    }
  }

  return { submit, loading: loadingUpdate || loadingUpload }
}
