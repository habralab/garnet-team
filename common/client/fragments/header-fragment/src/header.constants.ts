import { NavigationItem } from './header.interfaces'

export const getNavigationItems = (authUserId: string): NavigationItem[] => [
  {
    id: 'header.profile',
    url: `/user/${authUserId}`,
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
