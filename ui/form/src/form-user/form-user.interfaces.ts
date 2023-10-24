export interface FormUserValues {
  name: string
  surname: string
  description: string
  tags: string[]
  avatar: any
}

export interface FormUserProps {
  formValues: FormUserValues
  handleChange: (field: keyof FormUserValues) => (value: string) => void
}
