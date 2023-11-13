import { useRouter }  from 'next/router'
import { useEffect }  from 'react'
import { useState }   from 'react'

import { Team }       from '@shared/data'
import { useGetUser } from '@shared/data'
import { useSession } from '@stores/session'

import { useGetTeam } from '../data'

export const useTeamState = () => {
  const { query } = useRouter()
  const queryId = typeof query.id === 'string' ? query.id : ''

  const { userId } = useSession()

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

  const { user: ownerUser } = useGetUser({ id: team?.ownerUserId || '' })
  const joinRequestAuthUser = joinRequests.find((item) => item.userId === userId)

  useEffect(() => {
    if (fetchedTeam) {
      if (fetchedTeam.ownerUserId === userId) setIsMyTeam(true)

      setTeam(fetchedTeam)
    }
  }, [fetchedTeam, userId])

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
