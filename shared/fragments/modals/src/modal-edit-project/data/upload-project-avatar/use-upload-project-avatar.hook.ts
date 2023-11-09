import { useMutation }           from '@apollo/client'

import { Project }               from '@shared/data'

import { UPLOAD_PROJECT_AVATAR } from './upload-project-avatar.mutation'

export interface UploadProjectAvatarResponse {
  projectUploadAvatar: Project
}

export interface UploadProjectAvatarInput {
  id: Project['id']
  file: Blob
}

export const useUploadProjectAvatar = () => {
  const [uploadProjectAvatar, { data, loading }] = useMutation<
    UploadProjectAvatarResponse,
    UploadProjectAvatarInput
  >(UPLOAD_PROJECT_AVATAR)

  return { uploadProjectAvatar, data, loading }
}
