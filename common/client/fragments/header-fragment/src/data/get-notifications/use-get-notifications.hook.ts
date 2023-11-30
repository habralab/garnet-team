import { useQuery }          from '@apollo/client'

import { GET_NOTIFICATIONS } from './get-notifications.query'

// eslint-disable-next-line no-shadow
export enum NotificationTypes {
  ProjectEditOwner = 'ProjectEditOwner',
  TeamJoinProjectRequest = 'TeamJoinProjectRequest',
  TeamJoinRequestDecide = 'TeamJoinRequestDecide',
  TeamLeaveProject = 'TeamLeaveProject',
  TeamEditOwner = 'TeamEditOwner',
  TeamInvite = 'TeamInvite',
  TeamInviteDecide = 'TeamInviteDecide',
  TeamParticipantLeaveTeam = 'TeamParticipantLeaveTeam',
  TeamUserJoinRequest = 'TeamUserJoinRequest',
  TeamUserJoinRequestDecide = 'TeamUserJoinRequestDecide',
}

export interface Notification {
  id?: string
  userId?: string
  linkedEntityId?: string
  title?: string
  body?: string
  type?: NotificationTypes
  createdAt?: string
  linkedEntityAvatarUrl?: string
  linkedEntitiesNames?: string[]
  linkedRequestId?: string
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
    type: NotificationTypes.TeamJoinProjectRequest,
    userId: 'eea52408-40af-47d8-8c7b-e587f79fefc4',
    createdAt: '2023-11-28T13:30:34.646Z',
    linkedEntityId: '6565ea2925228e50861e89d0',
    linkedEntityAvatarUrl:
      'https://s3.timeweb.com/8a8879fe-garnet-stage/avatars/team/6565ea2925228e50861e89d0',
    linkedEntitiesNames: ['Mad matmers', 'Project entity'],
    linkedRequestId: '6565ea2925228e50861e89d0',
  },
  {
    id: '6565ed5125228e50861e89d7',
    title: 'Решение по заявке на вступление в проект',
    body: 'Владелец проекта Project entity принял заявку на вступление от команды Mad matmers',
    type: NotificationTypes.TeamJoinRequestDecide,
    userId: 'eea52408-40af-47d8-8c7b-e587f79fefc4',
    createdAt: '2023-11-28T13:38:25.119Z',
    linkedEntityId: '6565ea4f25228e50861e89d2',
    linkedEntityAvatarUrl:
      'https://s3.timeweb.com/8a8879fe-garnet-stage/avatars/team/6565ea2925228e50861e89d0',
    linkedEntitiesNames: ['Project entity', 'Mad matmers'],
  },
  {
    id: '65661415102a4e5c7901355c',
    title: 'Заявка на вступление в проект',
    body: 'Команда Mad matmers 2 хочет вступить в проект Project entity',
    type: NotificationTypes.TeamJoinProjectRequest,
    userId: 'eea52408-40af-47d8-8c7b-e587f79fefc4',
    createdAt: '2023-11-28T16:23:49.444Z',
    linkedEntityId: '656613d1102a4e5c79013559',
    linkedEntityAvatarUrl:
      'https://s3.timeweb.com/8a8879fe-garnet-stage/avatars/team/6565ea2925228e50861e89d0',
    linkedEntitiesNames: ['Project entity', 'Mad matmers'],
    linkedRequestId: '6565ea2925228e50861e89d0',
  },
]).flat()

export const useGetNotifications = () => {
  const { data, refetch } = useQuery<GetNotificationsResponse>(GET_NOTIFICATIONS)

  return {
    notifications: data?.notificationsGetListByCurrentUser?.notifications || mockNotifications,
    refetch,
  }
}
