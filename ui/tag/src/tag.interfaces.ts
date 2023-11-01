import { PropsWithChildren } from 'react'

import { BoxProps }          from '@ui/layout'

type TagVariant = 'primary' | 'secondary'

type TagSize = 'normal' | 'small'

export interface TagProps extends PropsWithChildren {
  variant?: TagVariant
  size?: TagSize
  close?: boolean
  onClick?: () => void
}

export interface TagElementProps extends BoxProps {
  variant: TagVariant
  size: TagSize
  hover: boolean
}
