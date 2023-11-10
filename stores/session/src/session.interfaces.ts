import { Session }   from '@ory/kratos-client'

import { ReactNode } from 'react'

export type SessionProviderProps = {
  children: ReactNode | ReactNode[]
}

export interface SessionContextProvider {
  session?: Session
  fullName: string
  userId?: string
  clearSession: () => void
  getSession: () => void
}
