import { ReactNode }   from 'react'

import { AvatarProps } from '@ui/avatar'

export type CardSize = 'large' | 'normal' | 'small'

export interface SizeConfig {
  size: number
  fontSize: string
  statisticGap: number
  avatarGroupShow: boolean
}

export interface CardProps {
  name?: string
  avatarUrl?: string
  countPeopleWord?: string
  countItemsWord?: string
  countItems?: number
  itemsAvatars?: string[]
  itemsAvatarsShape?: AvatarProps['shape']
  itemsIcon?: ReactNode
  cardSize?: CardSize
  url?: string
}
