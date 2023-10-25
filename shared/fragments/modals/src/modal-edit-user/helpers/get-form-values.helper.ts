import { User }           from '@shared/data'
import { FormUserValues } from '@ui/form'

export const getFormValues = (user?: User): FormUserValues => ({
  name: user?.userName?.split(' ')[0] ?? '',
  surname: user?.userName?.split(' ')[1] ?? '',
  description: user?.description ?? '',
  tags: user?.tags || [],
  avatar: user?.avatarUrl || '',
})
