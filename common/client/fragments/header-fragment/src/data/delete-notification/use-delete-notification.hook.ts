import { useMutation }         from '@apollo/client'

import { DELETE_NOTIFICATION } from './delete-notification.mutation'

export interface DeleteNotificationResponse {
  id?: string
}

export interface DeleteNotificationInput {
  id?: string
}

export const useDeleteNotification = () => {
  const [deleteNotification, { data, loading }] = useMutation<
    DeleteNotificationResponse,
    DeleteNotificationInput
  >(DELETE_NOTIFICATION)

  return { deleteNotification, data, loading }
}
