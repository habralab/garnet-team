import { useContext }             from 'react'

import { Context }                from './session.context'
import { SessionContextProvider } from './session.interfaces'

export const useSession = (): SessionContextProvider => {
  const context = useContext(Context)

  if (!context) throw Error('No session provider')

  return context
}
