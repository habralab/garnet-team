export interface FormTeamValues {
  name: string
  description: string
  tags: string[]
  avatar: any
}

export interface FormTeamProps {
  formValues: FormTeamValues
  handleChange: (field: keyof FormTeamValues) => (value: string | string[]) => void
}
