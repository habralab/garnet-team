import React               from 'react'

import { Background }      from '@ui/background'
import { WrapperMaxWidth } from '@ui/wrapper'
import { Header }          from '@user/header-fragment'
import { Profile }         from '@user/profile-fragment'

const UserPage = () => (
  <Background color='lightGreyTen' flexDirection='column' alignItems='center'>
    <Header />
    <WrapperMaxWidth>
      <Profile />
    </WrapperMaxWidth>
  </Background>
)

export default UserPage
