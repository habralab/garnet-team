import { Project } from '@shared/data'

export interface ModalEditProjectProps {
  modalOpen?: boolean
  onClose?: () => void
  onSubmit?: (project: Project) => void
  project?: Project
}
