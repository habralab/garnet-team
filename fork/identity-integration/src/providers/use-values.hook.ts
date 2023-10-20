import { useContext }    from 'react'

import { ValuesContext } from './values.context'
import { ValuesStore }   from './values.store'

export const useValues = (): ValuesStore => {
  const values = useContext(ValuesContext)

  if (!values) {
    throw new Error('Missing <ValuesProvider>')
  }

  return values
}
