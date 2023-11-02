export interface FormUserValues {
  name: string
  surname: string
  description: string
  tags: string[]
  avatar: string
}

export type FormUserErrorsText = Record<keyof FormUserValues, string>

export interface FormUserProps {
  formValues: FormUserValues
  handleChange: (field: keyof FormUserValues) => (value: string | string[]) => void
  handleDisabled?: (disabled: boolean) => void
}
