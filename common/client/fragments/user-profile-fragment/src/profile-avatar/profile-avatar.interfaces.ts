import { Team } from '@shared/data'
import { User } from '@shared/data'

export interface ProfileAvatarProps {
  user?: User
  isMyProfile: boolean
  teams: Team[]
}
