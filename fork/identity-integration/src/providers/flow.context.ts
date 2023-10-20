import { Identity }      from '@ory/kratos-client'
import { createContext } from 'react'

import { Flow }          from './flow.interfaces'

export interface ContextFlow {
  flow?: Flow
  loading: boolean
  identity?: Identity
  isValid?: boolean
}

const Context = createContext<ContextFlow>({ loading: false })

const { Provider, Consumer } = Context

export const FlowProvider = Provider
export const FlowConsumer = Consumer
export const FlowContext = Context
