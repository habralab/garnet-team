import { useEffect }  from 'react'
import { useState }   from 'react'

import { User }       from '@shared/data'
import { useSession } from '@stores/session'

export const useUserState = (fetchedUser?: User) => {
  const [user, setUser] = useState<User | undefined>(fetchedUser)
  const [isMyProfile, setIsMyProfile] = useState(false)

  const { userId } = useSession()

  useEffect(() => {
    if (fetchedUser) {
      setUser(fetchedUser)
      setIsMyProfile(fetchedUser.id === userId)
    }
  }, [userId, fetchedUser])

  return { user, setUser, isMyProfile }
}
