import { CSSProperties }     from 'react'
import { PropsWithChildren } from 'react'

export interface AvatarProps extends PropsWithChildren {
  shape?: 'circle' | 'square'
  size?: number
  image?: string
  url?: string
  title?: string
  color?: string
  style?: CSSProperties
}
