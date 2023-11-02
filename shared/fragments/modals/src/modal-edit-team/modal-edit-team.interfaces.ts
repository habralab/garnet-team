import { Team } from '@shared/data'

export interface ModalEditTeamProps {
  modalOpen?: boolean
  onClose?: () => void
  team?: Team
}
