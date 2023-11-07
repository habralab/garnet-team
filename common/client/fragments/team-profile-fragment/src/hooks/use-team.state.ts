import { useRouter }        from 'next/router'
import { useEffect }        from 'react'
import { useState }         from 'react'

import { Team }             from '@shared/data'
import { useGetAuthUserId } from '@shared/data'
import { useGetUser }       from '@shared/data'

import { useGetTeam }       from '../data'

export const useTeamState = () => {
  const router = useRouter()
  const queryId = typeof router.query.id === 'string' ? router.query.id : ''

  const { authUserId } = useGetAuthUserId()

  const [team, setTeam] = useState<Team>()
  const [isMyTeam, setIsMyTeam] = useState(false)

  const {
    team: fetchedTeam,
    teamProjects,
    teamParticipants,
    applicationParticipants,
    invitedParticipants,
    joinRequests,
  } = useGetTeam({ id: queryId, search: '', skip: 0, take: 0 })

  const { user: ownerUser } = useGetUser({ id: team?.ownerUserId || '', skip: 0, take: 20 })
  const joinRequestAuthUser = joinRequests.find((item) => item.userId === authUserId)

  useEffect(() => {
    if (fetchedTeam) {
      if (fetchedTeam.ownerUserId === authUserId) setIsMyTeam(true)

      setTeam(fetchedTeam)
    }
  }, [router, fetchedTeam, authUserId])

  return {
    team,
    setTeam,
    isMyTeam,
    ownerUser,
    teamProjects,
    teamParticipants,
    applicationParticipants,
    invitedParticipants,
    joinRequestAuthUser,
  }
}
