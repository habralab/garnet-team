import { Project }           from '@shared/data'
import { Team }              from '@shared/data'
import { FormProjectValues } from '@ui/form'
import { FormTeamValues }    from '@ui/form'

export const getFormValues = <T extends Team | Project>(
  item?: T
): T extends Team ? FormTeamValues : FormProjectValues => ({
  name: item?.name ?? '',
  description: item?.description ?? '',
  tags: item?.tags || [],
  avatar: item?.avatarUrl || '',
})
