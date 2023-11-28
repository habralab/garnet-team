import React               from 'react'

import { Header }          from '@app/header-fragment'
import { UserTeams }       from '@app/project-teams-fragment'
import { Background }      from '@ui/background'
import { WrapperMaxWidth } from '@ui/wrapper'

const ProjectTeamsPage = () => (
  <Background fill color='lightGreyTen' flexDirection='column' alignItems='center'>
    <Header />
    <WrapperMaxWidth>
      <UserTeams />
    </WrapperMaxWidth>
  </Background>
)

export default ProjectTeamsPage
