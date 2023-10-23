import { PropsWithChildren } from 'react'

export interface TagProps extends PropsWithChildren {
  variant?: 'primary' | 'secondary'
  close?: boolean
  onClick?: () => void
}
