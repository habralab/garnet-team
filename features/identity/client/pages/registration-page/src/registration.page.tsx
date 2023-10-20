import { RegistrationFlow } from '@atls/next-identity-integration/dist/src'

import React                from 'react'

import { RegistrationForm } from '@identity/auth-forms-fragment'
import { Background }       from '@ui/background/src'

const RegistrationPage = () => (
  <RegistrationFlow returnToUrl='/auth/confirmation'>
    <Background fill color='lightGreyForty' justifyContent='center' alignItems='center'>
      <RegistrationForm />
    </Background>
  </RegistrationFlow>
)

export default RegistrationPage
