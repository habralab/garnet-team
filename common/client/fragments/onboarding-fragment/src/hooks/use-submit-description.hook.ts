import { useUpdateUserDescription } from '../data'

export const useSubmitDescription = () => {
  const { updateUserDescription } = useUpdateUserDescription()

  const submit = async (value: string) => {
    try {
      if (value) {
        await updateUserDescription({ variables: { description: value } })
      }
    } catch (error) {
      /** @todo error notification */
    }
  }

  return { submit }
}
