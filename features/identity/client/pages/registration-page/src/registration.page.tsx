import { RegistrationFlow } from '@atls/next-identity-integration'

import React                from 'react'

import { RegistrationForm } from '@identity/auth-forms-fragment'
import { Header }           from '@identity/header-fragment'
import { Background }       from '@ui/background'
import { Column }           from '@ui/layout'

const RegistrationPage = () => (
  <Column fill>
    <Header />
    <Background fill color='lightGreyForty' justifyContent='center' alignItems='center'>
      <RegistrationFlow returnToUrl='/auth/confirmation'>
        <RegistrationForm />
      </RegistrationFlow>
    </Background>
  </Column>
)

export default RegistrationPage
