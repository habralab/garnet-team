import { LoginFlow }  from '@atls/next-identity-integration'

import React          from 'react'

import { LoginForm }  from '@identity/auth-forms-fragment'
import { Background } from '@ui/background'

const LoginPage = () => (
  <LoginFlow returnToUrl='/app'>
    <Background fill color='lightGreyForty' justifyContent='center' alignItems='center'>
      <LoginForm />
    </Background>
  </LoginFlow>
)

export default LoginPage
