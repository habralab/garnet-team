import { ContainerProps } from './container'

export interface ModalEditImageProps extends ContainerProps {
  modalOpen?: boolean
  onClose?: () => void
  onConfirm?: (url?: string) => void
  image?: string
}
