import React               from 'react'
import { FC }              from 'react'

import { UserProfile }     from '@app/user-profile-fragment'
import { User }            from '@shared/data'

import { useProfileState } from './hooks'

export const Profile: FC = () => {
  const { setUser, ...props } = useProfileState()

  const handleEditUser = (editedUser: User) => setUser(editedUser)

  return <UserProfile isMyProfile onEditUser={handleEditUser} {...props} />
}
