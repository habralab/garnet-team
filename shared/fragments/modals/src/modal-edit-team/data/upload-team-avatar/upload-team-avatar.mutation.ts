import { gql } from '@apollo/client'

export const UPLOAD_TEAM_AVATAR = gql`
  mutation TeamUploadAvatar($id: String!, $file: Upload!) {
    teamUploadAvatar(input: { teamId: $id, file: $file }) {
      id
      avatarUrl
    }
  }
`
