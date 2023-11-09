import { User } from '@shared/data'

export interface ModalEditUserProps {
  modalOpen?: boolean
  onClose?: () => void
  onSubmit?: (user: User) => void
  user?: User
}
