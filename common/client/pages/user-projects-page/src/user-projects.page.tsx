import React               from 'react'

import { Header }          from '@app/header-fragment'
import { UserProjects }    from '@app/user-projects-fragment'
import { Background }      from '@ui/background'
import { WrapperMaxWidth } from '@ui/wrapper'

const UserProjectsPage = () => (
  <Background fill color='lightGreyTen' flexDirection='column' alignItems='center'>
    <Header />
    <WrapperMaxWidth>
      <UserProjects />
    </WrapperMaxWidth>
  </Background>
)

export default UserProjectsPage
