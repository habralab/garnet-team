import { ModalProps as ModalComponentProps } from '@atls-ui-proto/modal'

import { PropsWithChildren }                 from 'react'

export interface ModalProps extends PropsWithChildren<ModalComponentProps> {
  theme?: 'primary' | 'exit'
  title?: string
  okText?: string
  cancelText?: string
  showOk?: boolean
  showCancel?: boolean
  onOk?: () => void
  onCancel?: () => void
}
