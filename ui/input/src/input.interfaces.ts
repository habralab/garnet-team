import { InputProps as BaseInputProps } from '@atls-ui-parts/input'
import { ReactNode }                    from 'react'

export interface InputProps extends BaseInputProps {
  errorText?: string
  horizontalLocation?: 'left' | 'right'
  iconSvg?: ReactNode
  valueWidth?: number | object
  valueHeight?: number | object
  onIconClick?: () => void
}
