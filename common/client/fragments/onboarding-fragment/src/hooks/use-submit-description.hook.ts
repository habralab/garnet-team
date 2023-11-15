import { useUpdateUserDescription } from '../data'

export const useSubmitDescription = () => {
  const { updateUserDescription, loading } = useUpdateUserDescription()

  const submit = async (value: string) => {
    try {
      if (value) {
        await updateUserDescription({ variables: { description: value } })
      }
    } catch (error) {
      /** @todo error notification */
    }
  }

  return { submit, loading }
}
