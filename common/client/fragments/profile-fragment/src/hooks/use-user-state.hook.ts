import { useRouter }        from 'next/router'
import { useEffect }        from 'react'
import { useState }         from 'react'

import { User }             from '@shared/data'
import { useGetAuthUserId } from '@shared/data'
import { useGetUser }       from '@shared/data'

export const useProfileState = () => {
  const [user, setUser] = useState<User>()

  const router = useRouter()

  const { authUserId } = useGetAuthUserId()

  const { user: fetchedUser, teams, projects } = useGetUser({ id: authUserId, skip: 0, take: 20 })

  useEffect(() => {
    if (fetchedUser) {
      const { avatarUrl, description, tags } = fetchedUser
      setUser(fetchedUser)

      if (!avatarUrl || !description || !tags || tags.length === 0) {
        router.push('/onboarding')
      }
    }
  }, [router, fetchedUser, authUserId])

  return { user, setUser, teams, projects }
}
