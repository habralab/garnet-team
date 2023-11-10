import { Session }              from '@ory/kratos-client'
import { useKratosClient }      from '@atls/next-identity-integration'

import React                    from 'react'
import axios                    from 'axios'
import { FC }                   from 'react'
import { useMemo }              from 'react'
import { useEffect }            from 'react'
import { useState }             from 'react'

import { Provider }             from './session.context'
import { SessionProviderProps } from './session.interfaces'

export const SessionProvider: FC<SessionProviderProps> = ({ children }) => {
  const [session, setSession] = useState<Session>()
  const cancelToken = useMemo(() => axios.CancelToken.source(), [])

  const { kratosClient } = useKratosClient()

  const fullName = useMemo(
    () => `${session?.identity.traits.name.first} ${session?.identity.traits.name.last}`,
    // eslint-disable-next-line react-hooks/exhaustive-deps
    [session?.id]
  )
  // eslint-disable-next-line react-hooks/exhaustive-deps
  const userId = useMemo(() => session?.identity.id, [session?.id])

  const clearSession = () => setSession(undefined)

  const getSession = async () => {
    kratosClient
      .toSession({}, { cancelToken: cancelToken.token })
      .then((response) => setSession(response.data))
      // eslint-disable-next-line no-console
      .catch((err) => console.log(err))
  }

  const cancelRequest = () => cancelToken.cancel()

  useEffect(() => {
    getSession()

    return cancelRequest
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [])

  return (
    <Provider value={{ session, fullName, userId, clearSession, getSession }}>{children}</Provider>
  )
}
