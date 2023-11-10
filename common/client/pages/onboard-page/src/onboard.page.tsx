import React          from 'react'

import { Header }     from '@app/header-fragment'
import { OnBoarding } from '@app/onboard-fragment'
import { Background } from '@ui/background'

const OnBoardPage = () => (
  <Background fill color='white' flexDirection='column' alignItems='center'>
    <Header disableNavigation />
    <OnBoarding />
  </Background>
)

export default OnBoardPage
