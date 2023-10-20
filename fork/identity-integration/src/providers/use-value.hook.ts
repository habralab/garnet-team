import { useState }    from 'react'
import { useEffect }   from 'react'
import { useCallback } from 'react'

import { useValues }   from './use-values.hook'

export const useValue = (name: string) => {
  const values = useValues()

  const [value, setValue] = useState(values.getValue(name))

  useEffect(() => {
    values.on(name, setValue)

    return () => {
      values.off(name, setValue)
    }
  }, [values, name])

  const onChange = useCallback((val) => values.setValue(name, val), [values, name])

  return [value, onChange]
}
