import { mockAuthUserId } from '@shared/data'

export const navigationItems = [
  {
    id: 'header.profile',
    url: `/user/${mockAuthUserId}`,
  },
  {
    id: 'header.teams',
    url: '/team',
  },
  {
    id: 'header.projects',
    url: '/project',
  },
]
