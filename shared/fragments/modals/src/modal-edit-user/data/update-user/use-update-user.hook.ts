import { useMutation } from '@apollo/client'

import { User }        from '@shared/data'

import { UPDATE_USER } from './update-user.mutation'

export interface UpdateUserResponse {
  userEditUsername: User
  userEditDescription: User
  userEditTags: User
}

export interface UpdateUserInput {
  userName: User['userName']
  description: User['description']
  tags: User['tags']
}

export const useUpdateUser = () => {
  const [updateUser, { data, loading }] = useMutation<UpdateUserResponse, UpdateUserInput>(
    UPDATE_USER
  )

  return { updateUser, data, loading }
}
