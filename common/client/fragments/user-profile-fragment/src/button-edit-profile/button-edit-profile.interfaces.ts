import { User }               from '@shared/data'
import { ModalEditUserProps } from '@shared/modals-fragment'

export interface ButtonEditProfileProps {
  user?: User
  onEditUser?: ModalEditUserProps['onSubmit']
}
