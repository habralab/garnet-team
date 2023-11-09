export interface FormProjectValues {
  name: string
  description: string
  tags: string[]
  avatar: string
}

export type FormProjectErrorsText = Record<keyof FormProjectValues, string>

export interface FormProjectProps {
  formValues: FormProjectValues
  handleChange: (field: keyof FormProjectValues) => (value: string | string[]) => void
  handleDisabled?: (disabled: boolean) => void
}
