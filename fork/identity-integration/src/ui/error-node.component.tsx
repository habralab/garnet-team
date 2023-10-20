import { ReactElement }    from 'react'
import { FC }              from 'react'

import { ActualFlowError } from './ui.interfaces'
import { useError }        from '../providers'

export interface ErrorNodeProps {
  children: (node: ActualFlowError) => ReactElement<any>
}

export const ErrorNode: FC<ErrorNodeProps> = ({ children }) => {
  const { error } = useError()

  if (error && typeof children === 'function') {
    return children(error as ActualFlowError)
  }

  return null
}
