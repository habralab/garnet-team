import { gql } from '@apollo/client'

export const UPLOAD_PROJECT_AVATAR = gql`
  mutation ProjectUploadAvatar($id: String!, $file: Upload!) {
    projectUploadAvatar(input: { projectId: $id, file: $file }) {
      id
      avatarUrl
    }
  }
`
