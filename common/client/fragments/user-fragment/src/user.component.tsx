import React                from 'react'
import { FC }               from 'react'

import { UserProfile }      from '@app/user-profile-fragment'
import { User as UserType } from '@shared/data'

import { useUserState }     from './hooks'

export const User: FC = () => {
  const { setUser, ...props } = useUserState()

  const handleEditUser = (editedUser: UserType) => setUser(editedUser)

  return <UserProfile onEditUser={handleEditUser} {...props} />
}
