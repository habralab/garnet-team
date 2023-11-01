import React               from 'react'

import { Header }          from '@app/header-fragment'
import { Team }            from '@app/team-fragment'
import { Background }      from '@ui/background'
import { WrapperMaxWidth } from '@ui/wrapper'

const TeamPage = () => (
  <Background fill color='lightGreyTen' flexDirection='column' alignItems='center'>
    <Header />
    <WrapperMaxWidth>
      <Team />
    </WrapperMaxWidth>
  </Background>
)

export default TeamPage
