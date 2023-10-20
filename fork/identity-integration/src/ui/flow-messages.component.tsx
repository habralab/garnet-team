import { UiText }       from '@ory/kratos-client'

import { ReactElement } from 'react'
import { FC }           from 'react'
import { useMemo }      from 'react'

import { useFlow }      from '../providers'

export interface FlowMessagesProps {
  children: (messages: UiText[]) => ReactElement<any>
}

export const FlowMessages: FC<FlowMessagesProps> = ({ children }) => {
  const { flow } = useFlow()
  const messages = useMemo(() => flow?.ui?.messages || [], [flow])

  if (typeof children === 'function' && messages) {
    return children(messages)
  }

  return null
}
