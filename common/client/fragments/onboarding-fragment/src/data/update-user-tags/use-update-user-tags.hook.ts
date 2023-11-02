import { useMutation }      from '@apollo/client'

import { User }             from '@shared/data'

import { UPDATE_USER_TAGS } from './update-user-tags.mutation'

export interface UpdateUserTagsResponse {
  userEditTags: User
}

export interface UpdateUserTagsInput {
  tags: User['tags']
}

export const useUpdateUserTags = () => {
  const [updateUserTags, { data, loading }] = useMutation<
    UpdateUserTagsResponse,
    UpdateUserTagsInput
  >(UPDATE_USER_TAGS)

  return { updateUserTags, data, loading }
}
