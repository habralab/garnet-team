import { PropsWithChildren } from 'react'

export interface ModalProps extends PropsWithChildren {
  theme?: 'primary' | 'exit'
  title?: string
  okText?: string
  cancelText?: string
  showOk?: boolean
  showCancel?: boolean
  onOk?: () => void
  onCancel?: () => void
  onClose?: () => void
  open?: boolean
}
