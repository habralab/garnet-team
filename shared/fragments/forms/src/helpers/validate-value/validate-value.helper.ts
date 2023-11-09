import { ValidateValue } from './validate-value.interfaces'

export const validateValue: ValidateValue = (formErrors, field, value, callback) => {
  const noValue = formErrors[field]?.noValue
  const minValue = formErrors[field]?.minValue
  const maxValue = formErrors[field]?.maxValue

  if (noValue && value.length === 0) {
    return callback(noValue.text)
  }

  if (minValue && minValue.number && value.length < minValue.number) {
    return callback(minValue.text, { number: minValue.number })
  }

  if (maxValue && maxValue.number && value.length >= maxValue.number) {
    return callback(maxValue.text, { number: maxValue.number })
  }

  return callback('')
}
