import React               from 'react'

import { Header }          from '@app/header-fragment'
import { Project }         from '@app/project-fragment'
import { Background }      from '@ui/background'
import { WrapperMaxWidth } from '@ui/wrapper'

const UserTeamsPage = () => (
  <Background fill color='lightGreyTen' flexDirection='column' alignItems='center'>
    <Header />
    <WrapperMaxWidth>
      <Project />
    </WrapperMaxWidth>
  </Background>
)

export default UserTeamsPage
