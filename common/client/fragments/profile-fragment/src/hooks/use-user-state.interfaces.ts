import { SetStateAction } from 'react'
import { Dispatch }       from 'react'

import { Team }           from '@shared/data'
import { Project }        from '@shared/data'
import { User }           from '@shared/data'

export type IsProfileFilled = (user: User | undefined) => boolean

export type UseProfileStateProps = () => {
  projects: Project[]
  teams: Team[]
  setUser: Dispatch<SetStateAction<User | undefined>>
  isNotFilled: boolean
  user: User | undefined
}
