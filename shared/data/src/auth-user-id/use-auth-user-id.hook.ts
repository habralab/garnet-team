import { useEffect }      from 'react'
import { useState }       from 'react'

import { mockAuthUserId } from '../mock'

export const useGetAuthUserId = () => {
  const [authUserId, setAuthUserId] = useState('')

  useEffect(() => {
    fetch('https://stage.garnet.pet-project.habr.com/kratos/sessions/whoami')
      .then((data) => data.json())
      .then((res) => setAuthUserId(res?.identity?.id || mockAuthUserId))
      .catch(() => setAuthUserId(mockAuthUserId))
  }, [])

  return { authUserId }
}
