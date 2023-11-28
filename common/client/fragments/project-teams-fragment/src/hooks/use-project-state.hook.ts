import { useRouter }          from 'next/router'
import { useEffect }          from 'react'
import { useState }           from 'react'

import { getUniqueTags }      from '@shared/utils'
import { useSession }         from '@stores/session'

import { useGetProject }      from '../data'
import { useGetJoinRequests } from '../data'

export const useProjectState = (setSelectedTags: (values: string[]) => void) => {
  const [isMyProject, setIsMyProject] = useState(false)

  const router = useRouter()
  const queryId = typeof router.query.id === 'string' ? router.query.id : ''

  const { userId } = useSession()

  const { projectTeams: teams, project } = useGetProject({ id: queryId })
  const { joinRequests, refetch } = useGetJoinRequests({ id: queryId })

  const uniqueTags = getUniqueTags(teams)

  useEffect(() => {
    if (project?.ownerUserId === userId) setIsMyProject(true)

    setSelectedTags(uniqueTags)

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [project, userId])

  return { project, isMyProject, teams, uniqueTags, joinRequests, refetch }
}
