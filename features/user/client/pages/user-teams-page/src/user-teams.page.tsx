import React          from 'react'

import { Background } from '@ui/background'
import { Wrapper }    from '@ui/background'
import { Header }     from '@user/header-fragment'
import { UserTeams }  from '@user/user-teams-fragment'

const UserTeamsPage = () => (
  <Background fill color='lightGreyTen' flexDirection='column' alignItems='center'>
    <Header />
    <Wrapper>
      <UserTeams />
    </Wrapper>
  </Background>
)

export default UserTeamsPage
