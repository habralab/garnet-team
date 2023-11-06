import { useState } from 'react'

import { sizes }    from '../enter-skills.config'

export const useSkillsState = (setError?: (text: string) => void) => {
  const [value, setValue] = useState<string[]>([])

  const handleChange = (newValue: string | string[]) => {
    if (Array.isArray(newValue)) setValue(newValue)

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
