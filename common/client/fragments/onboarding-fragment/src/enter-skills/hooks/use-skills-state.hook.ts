import { useState }      from 'react'
import { useIntl }       from 'react-intl'

import { sizes }         from '../enter-skills.config'
import { validateValue } from '../helpers'

export const useSkillsState = () => {
  const [errorText, setErrorText] = useState<string>('')
  const [value, setValue] = useState<string[]>([])

  const { formatMessage } = useIntl()

  const handleChange = (newValue: string | string[]) => {
    if (Array.isArray(newValue)) {
      setValue(newValue)
      validateValue(newValue, (id, values) => setErrorText(id ? formatMessage({ id }, values) : id))
    }
  }

  return {
    value,
    setValue: handleChange,
    errorText,
    disabled: value.length < sizes.min || value.length > sizes.max,
  }
}
