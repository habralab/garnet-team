import { NavigationItem } from './header.interfaces'

export const getNavigationItems = (): NavigationItem[] => [
  {
    id: 'header.profile',
    url: `/profile`,
  },
  {
    id: 'header.teams',
    url: '/teams',
  },
  {
    id: 'header.projects',
    url: '/projects',
  },
]
