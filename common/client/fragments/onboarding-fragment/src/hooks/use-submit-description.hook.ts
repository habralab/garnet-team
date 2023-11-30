import { useUpdateUserDescription } from '../data'

export const useSubmitDescription = () => {
  const { updateUserDescription, loading } = useUpdateUserDescription()

  const submit = async (value: string) => {
    try {
      if (value) {
        await updateUserDescription({ variables: { description: value } })
      }
    } catch (error) {
      if (process.env.NODE_ENV !== 'production') throw error
    }
  }

  return { submit, loading }
}
