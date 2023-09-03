import React                from 'react'

import { VerificationFlow } from '@fork/next-identity-integration'
import { VerificationLink } from '@identity/verification-link-fragment'

export const VerificationPage = () => (
  <VerificationFlow>
    <VerificationLink />
  </VerificationFlow>
)
