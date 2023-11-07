import { PropsWithChildren } from 'react'

import { ButtonProps }       from '@ui/button'

export interface ModalProps extends PropsWithChildren {
  theme?: 'primary' | 'exit'
  title?: string
  confirmText?: string
  cancelText?: string
  showConfirm?: boolean
  showCancel?: boolean
  onConfirm?: () => void
  onCancel?: () => void
  onClose?: () => void
  open?: boolean
  confirmProps?: ButtonProps
}
