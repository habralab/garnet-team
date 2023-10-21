import React          from 'react'

import { Background } from '@ui/background'
import { Wrapper }    from '@ui/background'
import { Header }     from '@user/header-fragment'
import { Profile }    from '@user/profile-fragment'

const UserPage = () => (
  <Background fill color='lightGreyTen' flexDirection='column' alignItems='center'>
    <Header />
    <Wrapper>
      <Profile />
    </Wrapper>
  </Background>
)

export default UserPage
