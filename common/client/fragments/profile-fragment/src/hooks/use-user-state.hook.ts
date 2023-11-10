import { useRouter }          from 'next/router'
import { useEffect }          from 'react'
import { useState }           from 'react'

import { User }               from '@shared/data'
import { useGetUser }         from '@shared/data'
import { useSession }         from '@stores/session'

import { isProfileNotFilled } from './use-user-state.helper'

export const useProfileState = () => {
  const [user, setUser] = useState<User>()

  const { push } = useRouter()

  const { userId } = useSession()

  const { user: fetchedUser, teams, projects } = useGetUser({ id: userId, skip: 0, take: 20 })

  useEffect(() => {
    if (fetchedUser) {
      setUser(fetchedUser)

      if (isProfileNotFilled(fetchedUser)) {
        push('/onboard')
      }
    }
  }, [push, fetchedUser, userId])

  return { user, setUser, teams, projects }
}
