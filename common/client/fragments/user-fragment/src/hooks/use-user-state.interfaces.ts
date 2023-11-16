import { Dispatch }       from 'react'
import { SetStateAction } from 'react'

import { Project }        from '@shared/data'
import { Team }           from '@shared/data'
import { User }           from '@shared/data'

export interface UseUserStateReturn {
  user?: User
  setUser: Dispatch<SetStateAction<User | undefined>>
  isMyProfile: boolean
  teams: Team[]
  projects: Project[]
}
