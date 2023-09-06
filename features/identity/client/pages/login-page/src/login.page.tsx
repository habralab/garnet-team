import React             from 'react'

import { LoginFlow }     from '@fork/next-identity-integration'
import { LoginPassword } from '@identity/login-password-fragment'

export const LoginPage = () => (
  <LoginFlow>
    <LoginPassword />
  </LoginFlow>
)
