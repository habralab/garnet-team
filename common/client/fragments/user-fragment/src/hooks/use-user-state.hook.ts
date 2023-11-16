import { useRouter }          from 'next/router'
import { useEffect }          from 'react'
import { useState }           from 'react'

import { User }               from '@shared/data'
import { useGetUser }         from '@shared/data'
import { useSession }         from '@stores/session'

import { UseUserStateReturn } from './use-user-state.interfaces'

export const useUserState = (): UseUserStateReturn => {
  const { query } = useRouter()
  const queryId = typeof query.id === 'string' ? query.id : ''

  const [user, setUser] = useState<User>()
  const [isMyProfile, setIsMyProfile] = useState<boolean>(false)

  const { userId } = useSession()

  const { user: fetchedUser, teams, projects } = useGetUser({ id: queryId, take: 20 })

  useEffect(() => {
    if (fetchedUser) {
      setUser(fetchedUser)
      setIsMyProfile(fetchedUser.id === userId)
    }
  }, [userId, fetchedUser])

  return { user, setUser, isMyProfile, teams, projects }
}
