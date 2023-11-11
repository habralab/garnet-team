import { useContext }             from 'react'

import { Context }                from './session.context'
import { SessionContextProvider } from './session.interfaces'

export const useSession = (): SessionContextProvider => useContext(Context)
