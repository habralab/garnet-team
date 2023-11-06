import { useUpdateUserTags } from '../data'

export const useSubmitSkills = () => {
  const { updateUserTags } = useUpdateUserTags()

  const submit = async (value: string[]) => {
    try {
      if (value.length !== 0) {
        await updateUserTags({ variables: { tags: value } })
      }
    } catch (error) {
      /** @todo error notification */
    }
  }

  return { submit }
}
