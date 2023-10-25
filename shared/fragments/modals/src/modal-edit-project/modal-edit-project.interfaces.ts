import { Project } from '@shared/data'

export interface ModalEditProjectProps {
  modalOpen?: boolean
  onClose?: () => void
  project?: Project
}
