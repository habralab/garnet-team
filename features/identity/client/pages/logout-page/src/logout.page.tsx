import React          from 'react'

import { LogoutFlow } from '@fork/next-identity-integration'
import { AuthLayout } from '@identity/auth-layout-fragment'

export const LogoutPage = () => (
  <LogoutFlow>
    <AuthLayout title='Выход' />
  </LogoutFlow>
)
