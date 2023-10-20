import { useContext }    from 'react'

import { SubmitContext } from './submit.context'
import { ContextSubmit } from './submit.context'

export const useSubmit = (): ContextSubmit => {
  const submit = useContext(SubmitContext)

  if (!submit) {
    throw new Error('Missing <SubmitProvider>')
  }

  return submit
}
