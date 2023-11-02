import React               from 'react'

import { Header }          from '@app/header-fragment'
import { Profile }         from '@app/profile-fragment'
import { Background }      from '@ui/background'
import { WrapperMaxWidth } from '@ui/wrapper'

const UserPage = () => (
  <Background fill color='lightGreyTen' flexDirection='column' alignItems='center'>
    <Header />
    <WrapperMaxWidth>
      <Profile />
    </WrapperMaxWidth>
  </Background>
)

export default UserPage
