import React               from 'react'

import { Header }          from '@app/header-fragment'
import { TeamInvite }      from '@app/team-invite-fragment'
import { Background }      from '@ui/background'
import { WrapperMaxWidth } from '@ui/wrapper'

const TeamInvitePage = () => (
  <Background fill color='lightGreyTen' flexDirection='column' alignItems='center'>
    <Header />
    <WrapperMaxWidth>
      <TeamInvite />
    </WrapperMaxWidth>
  </Background>
)

export default TeamInvitePage
