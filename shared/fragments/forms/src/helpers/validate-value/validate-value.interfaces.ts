type PrimitiveType = string | number | boolean | null | undefined

type Callback = (text: string, values?: Record<string, PrimitiveType>) => void

interface Error {
  text: string
  number?: number
}

type ErrorTypes = 'noValue' | 'minValue' | 'maxValue'

export type FormErrors = Partial<Record<string, Partial<Record<ErrorTypes, Error>>>>

export type ValidateValue = (
  formErrors: FormErrors,
  field: string,
  value: string | string[],
  callback: Callback
) => void
