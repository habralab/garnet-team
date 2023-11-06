import { useState } from 'react'

import { sizes }    from '../enter-description.config'

export const useDescriptionState = (setError?: (text: string) => void) => {
  const [value, setValue] = useState<string>('')

  const handleChange = (newValue: string) => {
    setValue(newValue)

    if (value.length >= sizes.min && value.length < sizes.max) {
      setError?.('')
    }
  }

  return {
    value,
    setValue: handleChange,
    disabled: value.length < sizes.min || value.length > sizes.max,
  }
}
