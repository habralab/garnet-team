import { RegistrationFlow } from '@atls/next-identity-integration'
import {
  RegistrationForm
}                           from '@identity/auth-forms-fragment/src/registration/registration-form.component'
import { Background }       from '@ui/background/src'

import React from 'react'

const RegistrationPage = () => (
  // @ts-ignore
  <RegistrationFlow>
    <Background fill color='lightGreyForty' justifyContent='center' alignItems='center'>
      <RegistrationForm/>
    </Background>
  </RegistrationFlow>
)

export default RegistrationPage
