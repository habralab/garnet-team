import { Team } from '@shared/data'
import { User } from '@shared/data'

export interface ProfileTeamsProps {
  ownerUser?: User
  teams: Team[]
  isMyProject: boolean
}
