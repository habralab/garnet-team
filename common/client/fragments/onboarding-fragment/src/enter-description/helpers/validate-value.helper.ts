import { errors } from '../enter-description.config'

type PrimitiveType = string | number | boolean | null | undefined

type Callback = (text: string, values?: Record<string, PrimitiveType>) => void

export const validateValue = (value: string, callback: Callback): void => {
  if (value.length === 0) {
    return callback(errors.noValue.text)
  }

  if (value.length < errors.minValue.number) {
    return callback(errors.minValue.text, { number: errors.minValue.number })
  }

  if (value.length >= errors.maxValue.number) {
    return callback(errors.maxValue.text, { number: errors.maxValue.number })
  }

  return callback('')
}
