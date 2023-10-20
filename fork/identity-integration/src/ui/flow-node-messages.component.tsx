import type { UiText }  from '@ory/kratos-client'

import { ReactElement } from 'react'
import { FC }           from 'react'

import { useFlowNode }  from '../providers'

export interface FlowNodeMessagesProps {
  name: string
  children: (messages: UiText[]) => ReactElement<any>
}

export const FlowNodeMessages: FC<FlowNodeMessagesProps> = ({ name, children }) => {
  const node = useFlowNode(name)

  if (typeof children === 'function' && node?.messages) {
    return children(node.messages)
  }

  return null
}
