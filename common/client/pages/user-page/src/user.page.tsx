import React               from 'react'

import { Header }          from '@app/header-fragment'
import { User }            from '@app/user-fragment'
import { Background }      from '@ui/background'
import { WrapperMaxWidth } from '@ui/wrapper'

const UserPage = () => (
  <Background fill color='lightGreyTen' flexDirection='column' alignItems='center'>
    <Header />
    <WrapperMaxWidth>
      <User />
    </WrapperMaxWidth>
  </Background>
)

export default UserPage
