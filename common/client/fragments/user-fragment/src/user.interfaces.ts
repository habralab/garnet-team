import { Project } from '@shared/data'
import { Team }    from '@shared/data'
import { User }    from '@shared/data'

export interface UserFragmentProps {
  user?: User
  teams: Team[]
  projects: Project[]
}
