import { useContext }  from 'react'

import { FlowContext } from './flow.context'
import { ContextFlow } from './flow.context'

export const useFlow = (): ContextFlow => {
  const flow = useContext(FlowContext)

  if (!flow) {
    throw new Error('Missing <FlowProvider>')
  }

  return flow
}
