import { useRouter }  from 'next/router'
import { useEffect }  from 'react'
import { useState }   from 'react'

import { User }       from '@shared/data'
import { useGetUser } from '@shared/data'
import { useSession } from '@stores/session'

export const useUserState = () => {
  const router = useRouter()
  const queryId = typeof router.query.id === 'string' ? router.query.id : ''

  const [user, setUser] = useState<User>()
  const [isMyProfile, setIsMyProfile] = useState(false)

  const { userId } = useSession()

  const { user: fetchedUser, teams, projects } = useGetUser({ id: queryId, take: 20 })

  useEffect(() => {
    if (fetchedUser) {
      setUser(fetchedUser)
      setIsMyProfile(fetchedUser.id === userId)
    }
  }, [userId, router, fetchedUser])

  return { user, setUser, isMyProfile, teams, projects }
}
