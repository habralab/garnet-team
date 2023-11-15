import React                 from 'react'
import { FC }                from 'react'

import { UserProfile }       from '@app/user-profile-fragment'
import { User as UserType }  from '@shared/data'

import { UserFragmentProps } from './user.interfaces'
import { useUserState }      from './hooks'

export const User: FC<UserFragmentProps> = ({ user: fetchedUser, teams, projects }) => {
  const { user, setUser, isMyProfile } = useUserState(fetchedUser)

  const handleEditUser = (editedUser: UserType) => setUser(editedUser)

  return (
    <UserProfile
      user={user}
      onEditUser={handleEditUser}
      isMyProfile={isMyProfile}
      teams={teams}
      projects={projects}
    />
  )
}
