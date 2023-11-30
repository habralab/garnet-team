import { useQuery }          from '@apollo/client'

import { Notification }      from '../data.interfaces'
import { GET_NOTIFICATIONS } from './get-notifications.query'

export interface GetNotificationsResponse {
  notificationsGetListByCurrentUser: {
    notifications?: Notification[]
  }
}

export const useGetNotifications = () => {
  const { data, refetch } = useQuery<GetNotificationsResponse>(GET_NOTIFICATIONS)

  return {
    notifications: data?.notificationsGetListByCurrentUser?.notifications || [],
    refetch,
  }
}
