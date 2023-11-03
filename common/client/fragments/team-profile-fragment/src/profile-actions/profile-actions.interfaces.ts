import { Team } from '@shared/data'

export interface ProfileActionsProps {
  team?: Team
  isMyTeam?: boolean
  hasJoinRequest?: boolean
  onEditTeam?: (team: Team) => void
}
