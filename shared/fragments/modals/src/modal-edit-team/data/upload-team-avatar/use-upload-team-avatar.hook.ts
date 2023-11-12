import { useMutation }        from '@apollo/client'

import { Team }               from '@shared/data'

import { UPLOAD_TEAM_AVATAR } from './upload-team-avatar.mutation'

export interface UploadTeamAvatarResponse {
  TeamUploadAvatar: Team
}

export interface UploadTeamAvatarInput {
  id: Team['id']
  file: Blob
}

export const useUploadTeamAvatar = () => {
  const [uploadTeamAvatar, { data, loading }] = useMutation<
    UploadTeamAvatarResponse,
    UploadTeamAvatarInput
  >(UPLOAD_TEAM_AVATAR)

  return { uploadTeamAvatar, data, loading }
}
