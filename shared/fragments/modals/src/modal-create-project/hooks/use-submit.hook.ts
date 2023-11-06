import { useRouter }         from 'next/router'

import { FormProjectValues } from '@shared/forms-fragment'

import { useCreateProject }  from '../data'

export const useSubmit = () => {
  const router = useRouter()

  const { createProject } = useCreateProject()

  const submit = async (formValues: FormProjectValues) => {
    try {
      const { avatar, description, name, tags } = formValues

      const avatarFile = await fetch(avatar).then((r) => r.blob())

      const { data } = await createProject({
        variables: {
          description,
          name,
          tags,
          avatar: avatarFile,
        },
      })

      router.push(`/project/${data?.projectCreate.id}`)
    } catch (error) {
      /** @todo error notification */
    }
  }

  return { submit }
}
