import { useMutation }             from '@apollo/client'

import { User }                    from '@shared/data'

import { UPDATE_USER_DESCRIPTION } from './update-user-description.mutation'

export interface UpdateUserDescriptionResponse {
  userEditDescription: User
}

export interface UpdateUserDescriptionInput {
  description: User['description']
}

export const useUpdateUserDescription = () => {
  const [updateUserDescription, { data, loading }] = useMutation<
    UpdateUserDescriptionResponse,
    UpdateUserDescriptionInput
  >(UPDATE_USER_DESCRIPTION)

  return { updateUserDescription, data, loading }
}
