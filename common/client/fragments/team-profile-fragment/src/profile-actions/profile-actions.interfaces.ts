import { JoinRequest } from '@shared/data'
import { Team }        from '@shared/data'

export interface ProfileActionsProps {
  team?: Team
  isMyTeam?: boolean
  joinRequest?: JoinRequest
  onEditTeam?: (team: Team) => void
}
