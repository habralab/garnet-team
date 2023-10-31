import React          from 'react'

import { Header }     from '@app/header-fragment'
import { Onboarding } from '@app/onboarding-fragment'
import { Background } from '@ui/background'

const UserPage = () => (
  <Background fill color='white' flexDirection='column' alignItems='center'>
    <Header disableNavigation />
    <Onboarding />
  </Background>
)

export default UserPage
