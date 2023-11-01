import { Project } from '@shared/data'
import { User }    from '@shared/data'

export interface ProfileProjectsProps {
  ownerUser?: User
  projects: Project[]
  isMyTeam: boolean
}
