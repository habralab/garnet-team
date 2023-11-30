import { gql } from '@apollo/client'

export const DELETE_NOTIFICATION = gql`
  mutation NotificationDelete($id: String!) {
    notificationDelete(input: { notificationId: $id }) {
      id
    }
  }
`
