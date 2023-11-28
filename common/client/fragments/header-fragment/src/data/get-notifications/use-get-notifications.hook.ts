import { useQuery }          from '@apollo/client'

import { GET_NOTIFICATIONS } from './get-notifications.query'

export interface Notification {
  id?: string
  userId?: string
  linkedEntityId?: string
  title?: string
  body?: string
  type?: string
  createdAt?: string
}

export interface GetNotificationsResponse {
  notificationsGetListByCurrentUser: {
    notifications?: Notification[]
  }
}

const mockNotifications: Notification[] = Array.from({ length: 4 }, () => [
  {
    id: '6565eb7a25228e50861e89d4',
    title: 'Заявка на вступление в проект',
    body: 'Команда Mad matmers хочет вступить в проект Project entity',
    type: 'TeamJoinProjectRequest',
    userId: 'eea52408-40af-47d8-8c7b-e587f79fefc4',
    createdAt: '2023-11-28T13:30:34.646Z',
    linkedEntityId: '6565ea2925228e50861e89d0',
  },
  {
    id: '6565ed5125228e50861e89d7',
    title: 'Решение по заявке на вступление в проект',
    body: 'Владелец проекта Project entity принял заявку на вступление от команды Mad matmers',
    type: 'TeamJoinRequestDecide',
    userId: 'eea52408-40af-47d8-8c7b-e587f79fefc4',
    createdAt: '2023-11-28T13:38:25.119Z',
    linkedEntityId: '6565ea4f25228e50861e89d2',
  },
  {
    id: '65661415102a4e5c7901355c',
    title: 'Заявка на вступление в проект',
    body: 'Команда Mad matmers 2 хочет вступить в проект Project entity',
    type: 'TeamJoinProjectRequest',
    userId: 'eea52408-40af-47d8-8c7b-e587f79fefc4',
    createdAt: '2023-11-28T16:23:49.444Z',
    linkedEntityId: '656613d1102a4e5c79013559',
  },
]).flat()

export const useGetNotifications = () => {
  const { data, refetch } = useQuery<GetNotificationsResponse>(GET_NOTIFICATIONS)

  return {
    notifications: data?.notificationsGetListByCurrentUser?.notifications || mockNotifications,
    refetch,
  }
}
