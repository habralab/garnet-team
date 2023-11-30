import React                  from 'react'
import { PropsWithChildren }  from 'react'
import { FC }                 from 'react'
import { useRouter }          from 'next/router'
import { useRef }             from 'react'
import { useEffect }          from 'react'

import { useGetUser }         from '@shared/data'
import { routes }             from '@shared/routes'
import { useSession }         from '@stores/session'

import { isProfileNotFilled } from './onboard-provider.helper'

export const OnboardProvider: FC<PropsWithChildren> = ({ children }) => {
  const { userId } = useSession()

  const isAlreadyOnboardRef = useRef<boolean>(false)

  const { user } = useGetUser({ id: userId })

  const { push } = useRouter()

  useEffect(() => {
    if (user && !isAlreadyOnboardRef.current && isProfileNotFilled(user)) {
      push(routes.onboard)
      isAlreadyOnboardRef.current = true
    }

    if (user && user.id !== userId) {
      isAlreadyOnboardRef.current = false
    }

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [user, userId])

  // eslint-disable-next-line react/jsx-no-useless-fragment
  return <>{children}</>
}
