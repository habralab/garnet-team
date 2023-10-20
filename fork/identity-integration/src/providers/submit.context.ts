import { createContext } from 'react'

import { Body } from './flow.interfaces'

export interface ContextSubmit {
  onSubmit: (override?: Partial<Body>) => void
  submitting: boolean
}

const Context = createContext<ContextSubmit>({
  submitting: false, onSubmit: () => ({}),
})

const { Provider, Consumer } = Context

export const SubmitProvider = Provider
export const SubmitConsumer = Consumer
export const SubmitContext = Context
