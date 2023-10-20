import { LogoutFlow as KratosLogoutFlow } from '@ory/kratos-client'
import { PropsWithChildren }              from 'react'

import React                              from 'react'
import { AxiosError }                     from 'axios'
import { FC }                             from 'react'
import { useRouter }                      from 'next/router'
import { useState }                       from 'react'
import { useEffect }                      from 'react'

import { kratos }                         from '../sdk'

interface LogoutFlowProps {
  returnToUrl?: string
}

export const LogoutFlow: FC<PropsWithChildren<LogoutFlowProps>> = ({ children, returnToUrl }) => {
  const [logoutToken, setLogoutToken] = useState<string>('')
  const router = useRouter()

  const { return_to: returnTo } = router.query

  useEffect(() => {
    if (!router.isReady) {
      return
    }

    kratos
      .createBrowserLogoutFlow(
        { returnTo: returnTo?.toString() ?? returnToUrl },
        { withCredentials: true }
      )
      .then(({ data }) => {
        setLogoutToken(data.logout_token)
      })
      .catch((error: AxiosError<KratosLogoutFlow>) => {
        // eslint-disable-next-line default-case
        switch (error.response?.status) {
          case 401:
            return router.push('/auth/login')
        }

        return Promise.reject(error)
      })
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [router, router.isReady])

  useEffect(() => {
    if (logoutToken) {
      kratos
        .updateLogoutFlow({ token: logoutToken }, { withCredentials: true })
        .then(() => router.reload())
    }
  }, [logoutToken, router])

  // eslint-disable-next-line react/jsx-no-useless-fragment
  return <>{children}</>
}
