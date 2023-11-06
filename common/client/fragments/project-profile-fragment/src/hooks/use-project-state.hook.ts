import { useRouter }      from 'next/router'
import { useEffect }      from 'react'
import { useState }       from 'react'

import { Project }        from '@shared/data'
import { mockAuthUserId } from '@shared/data'
import { useGetUser }     from '@shared/data'

import { useGetProject }  from '../data'

export const useProjectState = () => {
  const router = useRouter()
  const queryId = typeof router.query.id === 'string' ? router.query.id : ''

  const [project, setProject] = useState<Project>()

  const { projectTeams, project: fetchedProject } = useGetProject({ id: queryId })
  const { user: ownerUser } = useGetUser({ id: project?.ownerUserId || '', skip: 0, take: 20 })

  const [isMyProject, setIsMyProject] = useState(false)

  useEffect(() => {
    if (fetchedProject) {
      if (fetchedProject.ownerUserId === mockAuthUserId) setIsMyProject(true)

      setProject(fetchedProject)
    }
  }, [router, fetchedProject])

  return { project, setProject, isMyProject, projectTeams, ownerUser }
}
