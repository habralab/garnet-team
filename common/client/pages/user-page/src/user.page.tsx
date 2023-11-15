import React               from 'react'
import { FC }              from 'react'

import { Header }          from '@app/header-fragment'
import { User }            from '@app/user-fragment'
import { Background }      from '@ui/background'
import { WrapperMaxWidth } from '@ui/wrapper'

import { UserPageProps }   from './user.interfaces'

const UserPage: FC<UserPageProps> = (props) => (
  <Background fill color='lightGreyTen' flexDirection='column' alignItems='center'>
    <Header />
    <WrapperMaxWidth>
      <User {...props} />
    </WrapperMaxWidth>
  </Background>
)

export default UserPage
