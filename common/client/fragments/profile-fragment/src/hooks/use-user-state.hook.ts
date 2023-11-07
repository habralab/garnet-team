import { useRouter }      from 'next/router'
import { useEffect }      from 'react'
import { useState }       from 'react'

import { User }           from '@shared/data'
import { mockAuthUserId } from '@shared/data'
import { useGetUser }     from '@shared/data'

export const useUserState = () => {
  const router = useRouter()
  const queryId = typeof router.query.id === 'string' ? router.query.id : ''

  const [user, setUser] = useState<User>()
  const [isMyProfile, setIsMyProfile] = useState(false)

  const { user: fetchedUser, teams, projects } = useGetUser({ id: queryId, skip: 0, take: 20 })

  useEffect(() => {
    if (fetchedUser) {
      const { avatarUrl, description, id, tags } = fetchedUser

      if (!avatarUrl || !description || !tags || tags.length === 0) {
        router.push('/onboarding')
      }

      if (id === mockAuthUserId) setIsMyProfile(true)

      setUser(fetchedUser)
    }
  }, [router, fetchedUser])

  return { user, setUser, isMyProfile, teams, projects }
}
