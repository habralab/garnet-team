import { Team } from '@shared/data'

export interface ModalEditTeamProps {
  modalOpen?: boolean
  onClose?: () => void
  onSubmit?: (team: Team) => void
  team?: Team
}
