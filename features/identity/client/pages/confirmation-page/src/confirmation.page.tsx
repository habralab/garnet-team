import React                from 'react'

import { ConfirmationCard } from '@identity/confirmation-card-fragment'
import { Background }       from '@ui/background'

const ConfirmationPage = () => (
  <Background color='lightGreyForty' fill justifyContent='center' alignItems='center'>
    <ConfirmationCard />
  </Background>
)

export default ConfirmationPage
