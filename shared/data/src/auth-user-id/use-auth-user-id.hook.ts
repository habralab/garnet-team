import { Session }           from '@ory/kratos-client'
import { useKratosClient }   from '@atls/next-identity-integration'

import axios                 from 'axios'
import { useEffect }         from 'react'
import { useState }          from 'react'

import { UseAuthUserIdHook } from './use-auth-user-id.interfaces'

export const useGetAuthUserId: UseAuthUserIdHook = () => {
  const [session, setSession] = useState<Session>()

  const { kratosClient } = useKratosClient()

  useEffect(() => {
    const source = axios.CancelToken.source()

    kratosClient
      .toSession({}, { cancelToken: source.token })
      .then((response) => setSession(response.data))
      // eslint-disable-next-line no-console
      .catch((err) => console.log(err))

    return () => source.cancel()
  }, [kratosClient])

  return { session, authUserId: session?.identity.id }
}
