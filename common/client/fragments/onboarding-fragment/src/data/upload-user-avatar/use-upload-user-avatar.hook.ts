import { useMutation }        from '@apollo/client'

import { User }               from '@shared/data'

import { UPLOAD_USER_AVATAR } from './upload-user-avatar.mutation'

export interface UploadUserAvatarResponse {
  userUploadAvatar: User
}

export interface UploadUserAvatarInput {
  file: Blob
}

export const useUploadUserAvatar = () => {
  const [uploadUserAvatar, { data, loading }] = useMutation<
    UploadUserAvatarResponse,
    UploadUserAvatarInput
  >(UPLOAD_USER_AVATAR)

  return { uploadUserAvatar, data, loading }
}
