import { ContainerProps } from './container'

export interface ModalEditImageProps extends ContainerProps {
  modalOpen?: boolean
  onClose?: () => void
  onConfirm?: (url?: string, blob?: Blob) => void
  image?: string
}
