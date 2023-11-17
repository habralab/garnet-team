import { routes }         from '@shared/routes'

import { NavigationItem } from './header.interfaces'

export const navigationItems: NavigationItem[] = [
  {
    id: 'header.profile',
    url: routes.profile,
  },
  {
    id: 'header.teams',
    url: routes.teams,
  },
  {
    id: 'header.projects',
    url: routes.projects,
  },
]
