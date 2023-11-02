import { Project }           from '@shared/data'
import { Team }              from '@shared/data'
import { FormProjectValues } from '@shared/forms-fragment'
import { FormTeamValues }    from '@shared/forms-fragment'

export const getFormValues = <T extends Team | Project>(
  item?: T
): T extends Team ? FormTeamValues : FormProjectValues => {
  if (!item) {
    return { name: '', description: '', tags: [], avatar: '' }
  }

  let name: string | undefined

  if ('projectName' in item) {
    name = item.projectName
  } else if ('name' in item) {
    name = item.name
  }

  return {
    name: name ?? '',
    description: item?.description ?? '',
    tags: item?.tags || [],
    avatar: item?.avatarUrl || '',
  }
}
