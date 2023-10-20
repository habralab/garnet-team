import { LoginForm } from '@identity/auth-forms-fragment'
import React         from 'react'

import { LoginFlow } from '@fork/identity-integration'
import { Background } from '@ui/background'

const LoginPage = () => (
  <LoginFlow returnToUrl='/app'>
    <Background fill color='lightGreyForty' justifyContent='center' alignItems='center'>
      <LoginForm/>
    </Background>
  </LoginFlow>
)

export default LoginPage
