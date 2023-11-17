import { useRouter }         from 'next/router'

import { FormProjectValues } from '@shared/forms-fragment'
import { routes }            from '@shared/routes'

import { useCreateProject }  from '../data'

export const useSubmit = () => {
  const router = useRouter()

  const { createProject, loading } = useCreateProject()

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

      router.push(`${routes.projects}/${data?.projectCreate.id}`)
    } catch (error) {
      /** @todo error notification */
    }
  }

  return { submit, loading }
}
