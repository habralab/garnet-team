import { gql } from '@apollo/client'

export const UPLOAD_USER_AVATAR = gql`
  mutation UserUploadAvatar($file: Upload!) {
    userUploadAvatar(input: { file: $file }) {
      id
      avatarUrl
    }
  }
`
