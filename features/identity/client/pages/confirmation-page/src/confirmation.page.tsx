import React                from 'react'

import { ConfirmationCard } from '@identity/confirmation-card-fragment'
import { Header }           from '@identity/header-fragment'
import { Background }       from '@ui/background'
import { Column }           from '@ui/layout'

const ConfirmationPage = () => (
  <Column fill>
    <Header />
    <Background color='lightGreyForty' fill justifyContent='center' alignItems='center'>
      <ConfirmationCard />
    </Background>
  </Column>
)

export default ConfirmationPage
