import { Team } from '@shared/data'

export interface ButtonEditTeamProps {
  team?: Team
  onEditTeam?: (team: Team) => void
}
