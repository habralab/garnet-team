import { useUpdateUserTags } from '../data'

export const useSubmitSkills = () => {
  const { updateUserTags, loading } = useUpdateUserTags()

  const submit = async (value: string[]) => {
    try {
      if (value.length !== 0) {
        await updateUserTags({ variables: { tags: value } })
      }
    } catch (error) {
      if (process.env.NODE_ENV !== 'production') throw error
    }
  }

  return { submit, loading }
}
