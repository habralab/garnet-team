import { useUploadUserAvatar } from '../data'

export const useSubmitAvatar = () => {
  const { uploadUserAvatar, loading } = useUploadUserAvatar()

  const submit = async (blob?: Blob) => {
    try {
      if (blob) {
        await uploadUserAvatar({ variables: { file: blob } })
      }
    } catch (error) {
      /** @todo error notification */
    }
  }

  return { submit, loading }
}
