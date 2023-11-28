import { gql } from '@apollo/client'

export const GET_NOTIFICATIONS = gql`
  query NotificationGet {
    notificationsGetListByCurrentUser {
      notifications {
        id
        title
        body
        type
        userId
        createdAt
        linkedEntityId
      }
    }
  }
`
