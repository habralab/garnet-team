import { Team }           from '@shared/data'
import { FormTeamValues } from '@ui/form'

export const getFormValues = (team?: Team): FormTeamValues => ({
  name: team?.name ?? '',
  description: team?.description ?? '',
  tags: team?.tags || [],
  avatar: team?.avatarUrl || '',
})
