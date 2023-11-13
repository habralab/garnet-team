import { useRouter }     from 'next/router'
import { useEffect }     from 'react'
import { useState }      from 'react'

import { useGetUser }    from '@shared/data'
import { getUniqueTags } from '@shared/helpers'
import { useSession }    from '@stores/session'

export const useUserTeamsState = (setSelectedTags: (values: string[]) => void) => {
  const [isMyProfile, setIsMyProfile] = useState(false)

  const router = useRouter()
  const queryId = typeof router.query.id === 'string' ? router.query.id : ''

  const { userId } = useSession()

  const { user, teams } = useGetUser({ id: queryId })

  const uniqueTags = getUniqueTags(teams)

  useEffect(() => {
    if (user?.id === userId) setIsMyProfile(true)

    setSelectedTags(uniqueTags)

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [user, userId])

  return { teams, isMyProfile, uniqueTags }
}
