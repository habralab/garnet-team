import React               from 'react'

import { Background }      from '@ui/background'
import { WrapperMaxWidth } from '@ui/wrapper'
import { Header }          from '@user/header-fragment'
import { UserTeams }       from '@user/user-teams-fragment'

const UserTeamsPage = () => (
  <Background fill color='lightGreyTen' flexDirection='column' alignItems='center'>
    <Header />
    <WrapperMaxWidth>
      <UserTeams />
    </WrapperMaxWidth>
  </Background>
)

export default UserTeamsPage
