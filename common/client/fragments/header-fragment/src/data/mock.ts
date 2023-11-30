import { Notification }      from './data.interfaces'
import { NotificationTypes } from './data.interfaces'

export const mockNotifications: Notification[] = Array.from({ length: 4 }, () => [
  {
    id: '6565eb7a25228e50861e89d4',
    title: 'Заявка на вступление в проект',
    body: 'Команда Mad matmers хочет вступить в проект Project entity',
    type: NotificationTypes.TeamJoinProjectRequest,
    userId: 'eea52408-40af-47d8-8c7b-e587f79fefc4',
    createdAt: '2023-11-28T13:30:34.646Z',
    linkedEntityId: '6565ea2925228e50861e89d0',
    quotedEntities: [
      {
        avatarUrl:
          'https://s3.timeweb.com/8a8879fe-garnet-stage/avatars/team/6565ea2925228e50861e89d0',
        quote: 'Mad matmers',
        id: '6565ea2925228e50861e89d0',
      },
      {
        avatarUrl:
          'https://s3.timeweb.com/8a8879fe-garnet-stage/avatars/team/6565ea2925228e50861e89d0',
        quote: 'Project entity',
        id: '6565ea2925228e50861e89d1',
      },
    ],
  },
  {
    id: '6565ed5125228e50861e89d7',
    title: 'Решение по заявке на вступление в проект',
    body: 'Владелец проекта Project entity принял заявку на вступление от команды Mad matmers',
    type: NotificationTypes.TeamJoinRequestDecide,
    userId: 'eea52408-40af-47d8-8c7b-e587f79fefc4',
    createdAt: '2023-11-28T13:38:25.119Z',
    quotedEntities: [
      {
        avatarUrl:
          'https://s3.timeweb.com/8a8879fe-garnet-stage/avatars/team/6565ea2925228e50861e89d0',
        quote: 'Mad matmers',
        id: '6565ea2925228e50861e89d0',
      },
      {
        avatarUrl:
          'https://s3.timeweb.com/8a8879fe-garnet-stage/avatars/team/6565ea2925228e50861e89d0',
        quote: 'Project entity',
        id: '6565ea2925228e50861e89d1',
      },
    ],
  },
  {
    id: '65661415102a4e5c7901355c',
    title: 'Заявка на вступление в проект',
    body: 'Команда Mad matmers 2 хочет вступить в проект Project entity',
    type: NotificationTypes.TeamJoinProjectRequest,
    userId: 'eea52408-40af-47d8-8c7b-e587f79fefc4',
    createdAt: '2023-11-28T16:23:49.444Z',
    linkedEntityId: '656613d1102a4e5c79013559',
    quotedEntities: [
      {
        avatarUrl:
          'https://s3.timeweb.com/8a8879fe-garnet-stage/avatars/team/6565ea2925228e50861e89d0',
        quote: 'Mad matmers',
        id: '6565ea2925228e50861e89d0',
      },
      {
        avatarUrl:
          'https://s3.timeweb.com/8a8879fe-garnet-stage/avatars/team/6565ea2925228e50861e89d0',
        quote: 'Project entity',
        id: '6565ea2925228e50861e89d1',
      },
    ],
  },
]).flat()
