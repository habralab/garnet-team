import { LoginFlow }  from '@atls/next-identity-integration'

import React          from 'react'

import { LoginForm }  from '@identity/auth-forms-fragment'
import { Header }     from '@identity/header-fragment'
import { Background } from '@ui/background'
import { Column }     from '@ui/layout'

const LoginPage = () => (
  <Column fill>
    <Header />
    <Background fill color='lightGreyForty' justifyContent='center' alignItems='center'>
      <LoginFlow returnToUrl='/app'>
        <LoginForm />
      </LoginFlow>
    </Background>
  </Column>
)

export default LoginPage
