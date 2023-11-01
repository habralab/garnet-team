import { User } from '@shared/data'

export interface ModalEditUserProps {
  modalOpen?: boolean
  onClose?: () => void
  user?: User
}
