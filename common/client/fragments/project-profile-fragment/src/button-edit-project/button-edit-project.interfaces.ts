import { Project }               from '@shared/data'
import { ModalEditProjectProps } from '@shared/modals-fragment'

export interface ButtonEditProjectProps {
  project?: Project
  onEditProject?: ModalEditProjectProps['onSubmit']
}
