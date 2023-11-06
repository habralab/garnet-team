import { mockAuthUserId } from '@shared/data'

import { NavigationItem } from './header.interfaces'

export const navigationItems: NavigationItem[] = [
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
