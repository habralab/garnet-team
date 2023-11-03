import React               from 'react'

import { Header }          from '@app/header-fragment'
import { TeamProfile }     from '@app/team-profile-fragment'
import { Background }      from '@ui/background'
import { WrapperMaxWidth } from '@ui/wrapper'

const TeamProfilePage = () => (
  <Background fill color='lightGreyTen' flexDirection='column' alignItems='center'>
    <Header />
    <WrapperMaxWidth>
      <TeamProfile />
    </WrapperMaxWidth>
  </Background>
)

export default TeamProfilePage
