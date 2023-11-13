import { useRouter }      from 'next/router'

import { FormTeamValues } from '@shared/forms-fragment'
import { routes }         from '@shared/routes'

import { useCreateTeam }  from '../data'

export const useSubmit = () => {
  const router = useRouter()

  const { createTeam } = useCreateTeam()

  const submit = async (formValues: FormTeamValues) => {
    try {
      const { avatar, description, name, tags } = formValues

      const avatarFile = await fetch(avatar).then((r) => r.blob())

      const { data } = await createTeam({
        variables: {
          description,
          name,
          tags,
          avatar: avatarFile,
        },
      })

      router.push(`${routes.teams}/${data?.teamCreate.id}`)
    } catch (error) {
      /** @todo error notification */
    }
  }

  return { submit }
}
