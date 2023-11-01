import { Project }           from '@shared/data'
import { Team }              from '@shared/data'
import { FormProjectValues } from '@shared/forms-fragment'
import { FormTeamValues }    from '@shared/forms-fragment'

export const getFormValues = <T extends Team | Project>(
  item?: T
): T extends Team ? FormTeamValues : FormProjectValues => ({
  name: item?.name ?? '',
  description: item?.description ?? '',
  tags: item?.tags || [],
  avatar: item?.avatarUrl || '',
})
