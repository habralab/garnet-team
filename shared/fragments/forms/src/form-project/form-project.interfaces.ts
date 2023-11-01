export interface FormProjectValues {
  name: string
  description: string
  tags: string[]
  avatar: string
}

export interface FormProjectProps {
  formValues: FormProjectValues
  handleChange: (field: keyof FormProjectValues) => (value: string | string[]) => void
}
