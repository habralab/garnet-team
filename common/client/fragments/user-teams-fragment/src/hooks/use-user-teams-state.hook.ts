import { useRouter }      from 'next/router'
import { useEffect }      from 'react'
import { useState }       from 'react'

import { mockAuthUserId } from '@shared/data'
import { useGetUser }     from '@shared/data'
import { getUniqueTags }  from '@shared/helpers'

export const useUserTeamsState = (setSelectedTags: (values: string[]) => void) => {
  const [isMyProfile, setIsMyProfile] = useState(false)

  const router = useRouter()
  const queryId = typeof router.query.id === 'string' ? router.query.id : ''

  const { user, teams } = useGetUser({ id: queryId, skip: 0, take: 20 })

  const uniqueTags = getUniqueTags(teams)

  useEffect(() => {
    if (user?.id === mockAuthUserId) setIsMyProfile(true)

    setSelectedTags(uniqueTags)

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [user])

  return { teams, isMyProfile, uniqueTags }
}
