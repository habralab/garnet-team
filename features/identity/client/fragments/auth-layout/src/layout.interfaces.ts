import { ReactElement } from 'react'

export type LayoutPosition = 'left' | 'right'
export type LayoutType = 'sidebar' | 'panel' | 'center'

export interface LayoutProps {
  title?: string | ReactElement
  actions?: string | ReactElement
}

export interface SidebarLayoutProps extends LayoutProps {
  position?: LayoutPosition
}

export interface AuthLayoutProps extends LayoutProps {
  position?: LayoutPosition
  type?: LayoutType
}
