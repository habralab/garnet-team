export interface FormTeamValues {
  name: string
  description: string
  tags: string[]
  avatar: string
}

export type FormTeamErrorsText = Record<keyof FormTeamValues, string>

export interface FormTeamProps {
  formValues: FormTeamValues
  handleChange: (field: keyof FormTeamValues) => (value: string | string[]) => void
  handleDisabled?: (disabled: boolean) => void
}
