import { Project } from '@shared/data'
import { Team }    from '@shared/data'
import { User }    from '@shared/data'

export interface UserPageProps {
  user?: User
  teams: Team[]
  projects: Project[]
}
