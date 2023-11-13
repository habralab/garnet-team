import { useRouter }            from 'next/router'
import { useMemo }              from 'react'
import { useEffect }            from 'react'
import { useState }             from 'react'

import { User }                 from '@shared/data'
import { useGetUser }           from '@shared/data'
import { useSession }           from '@stores/session'

import { UseProfileStateProps } from './use-user-state.interfaces'
import { isProfileNotFilled }   from './use-user-state.helper'

export const useProfileState: UseProfileStateProps = () => {
  const [user, setUser] = useState<User>()

  const { push } = useRouter()

  const { userId } = useSession()

  const { user: fetchedUser, teams, projects } = useGetUser({ id: userId, take: 20 })

  const isNotFilled = useMemo(() => isProfileNotFilled(fetchedUser), [fetchedUser])

  useEffect(() => {
    if (fetchedUser) {
      setUser(fetchedUser)

      if (isNotFilled) push('/onboard')
    }
  }, [push, fetchedUser, userId, isNotFilled])

  return { user, setUser, teams, projects, isNotFilled }
}
