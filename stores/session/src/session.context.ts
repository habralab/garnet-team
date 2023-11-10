import { createContext }          from 'react'

import { SessionContextProvider } from './session.interfaces'

export const Context = createContext<SessionContextProvider>({} as SessionContextProvider)

export const { Provider, Consumer } = Context
