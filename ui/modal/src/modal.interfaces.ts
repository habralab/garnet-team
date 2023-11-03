import { PropsWithChildren } from 'react'

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
}
