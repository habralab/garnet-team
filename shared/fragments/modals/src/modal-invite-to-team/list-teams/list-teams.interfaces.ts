import { Team } from '@shared/data'

export interface ListTeamsProps {
  teams: Team[]
  selectedTeam?: string
  onChangeSelectedTeam: (id?: string) => void
}
