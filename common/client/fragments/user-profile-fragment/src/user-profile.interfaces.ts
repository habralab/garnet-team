import { Project } from '@shared/data'
import { Team }    from '@shared/data'
import { User }    from '@shared/data'

export interface UserProfileProps {
  user?: User
  projects: Project[]
  teams: Team[]
  isMyProfile: boolean
  onEditUser: (editedUser: User) => void
}
