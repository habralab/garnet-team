import { useEffect }            from 'react'
import { useState }             from 'react'

import { User }                 from '@shared/data'
import { useGetUser }           from '@shared/data'
import { useSession }           from '@stores/session'

import { UseProfileStateProps } from './use-profile-state.interfaces'

export const useProfileState: UseProfileStateProps = () => {
  const [user, setUser] = useState<User>()

  const { userId } = useSession()

  const { user: fetchedUser, teams, projects } = useGetUser({ id: userId, take: 20 })

  useEffect(() => {
    if (fetchedUser) setUser(fetchedUser)
  }, [fetchedUser, userId])

  return { user, setUser, teams, projects }
}
