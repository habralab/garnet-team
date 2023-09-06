import React               from 'react'
import { FC }              from 'react'

import { AuthLayoutProps } from './layout.interfaces'
import { SidebarLayout }   from './sidebar-layout.component'

export const AuthLayout: FC<AuthLayoutProps> = ({ type, ...props }) => {
  if (type === 'sidebar') {
    return <SidebarLayout {...props} />
  }

  return <SidebarLayout {...props} />
}
