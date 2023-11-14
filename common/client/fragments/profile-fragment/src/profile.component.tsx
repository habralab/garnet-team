import React               from 'react'
import { FC }              from 'react'

import { UserProfile }     from '@app/user-profile-fragment'
import { User }            from '@shared/data'
import { Condition }       from '@ui/condition'

import { useProfileState } from './hooks'

export const Profile: FC = () => {
  const { setUser, isNotFilled, ...props } = useProfileState()

  const handleEditUser = (editedUser: User) => setUser(editedUser)

  return (
    <Condition match={!isNotFilled}>
      <UserProfile isMyProfile onEditUser={handleEditUser} {...props} />
    </Condition>
  )
}
