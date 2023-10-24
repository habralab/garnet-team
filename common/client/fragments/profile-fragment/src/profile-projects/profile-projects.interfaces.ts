import { Project } from '@shared/data'
import { User }    from '@shared/data'

export interface ProfileProjectsProps {
  user?: User
  projects: Project[]
  isMyProfile: boolean
}
