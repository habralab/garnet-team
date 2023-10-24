export interface FormProjectValues {
  name: string
  description: string
  tags: string[]
  avatar: any
}

export interface FormProjectProps {
  formValues: FormProjectValues
  handleChange: (field: keyof FormProjectValues) => (value: string) => void
}
