import { ReactElement } from 'react'
import { FC }           from 'react'

import { Body }         from '../providers'
import { useSubmit }    from '../providers'

export interface FlowSubmitProps {
  children: (submit: {
    onSubmit: (override?: Partial<Body>) => void
    submitting: boolean
    restartFlow?: () => void
  }) => ReactElement<any>
}

export const FlowSubmit: FC<FlowSubmitProps> = ({ children }) => {
  const { submitting, onSubmit } = useSubmit()

  if (typeof children === 'function') {
    return children({
      submitting,
      onSubmit: (override?: Partial<Body>) => onSubmit(override),
    })
  }

  return null
}
