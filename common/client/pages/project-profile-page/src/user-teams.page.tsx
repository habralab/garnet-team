import React               from 'react'

import { Header }          from '@app/header-fragment'
import { ProjectProfile }  from '@app/project-profile-fragment'
import { Background }      from '@ui/background'
import { WrapperMaxWidth } from '@ui/wrapper'

const UserTeamsPage = () => (
  <Background fill color='lightGreyTen' flexDirection='column' alignItems='center'>
    <Header />
    <WrapperMaxWidth>
      <ProjectProfile />
    </WrapperMaxWidth>
  </Background>
)

export default UserTeamsPage
