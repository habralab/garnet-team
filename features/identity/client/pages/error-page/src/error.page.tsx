import React          from 'react'

import { ErrorFlow }  from '@fork/next-identity-integration'
import { AuthLayout } from '@identity/auth-layout-fragment'
import { Error }      from '@identity/error-fragment'

export const ErrorPage = () => (
  <ErrorFlow>
    <AuthLayout>
      <Error />
    </AuthLayout>
  </ErrorFlow>
)
