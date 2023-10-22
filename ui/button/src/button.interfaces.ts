import { ButtonProps as BaseButtonProps } from '@atls-ui-parts/button'

import { ReactNode }                      from 'react'

export type ButtonVariant = 'primary' | 'secondary'

export type ButtonSize = 'normal'

export interface ButtonProps extends BaseButtonProps {
  variant?: ButtonVariant
  size?: ButtonSize
  active?: boolean
  ref?: any
  iconSvg?: ReactNode
  valueWidth?: number | object
  valueHeight?: number | object
  horizontalLocation?: 'left' | 'right'
}
