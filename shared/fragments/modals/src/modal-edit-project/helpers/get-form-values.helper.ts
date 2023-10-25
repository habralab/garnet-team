import { Project }           from '@shared/data'
import { FormProjectValues } from '@ui/form'

export const getFormValues = (project?: Project): FormProjectValues => ({
  name: project?.name ?? '',
  description: project?.description ?? '',
  tags: project?.tags || [],
  avatar: project?.avatarUrl || '',
})
