import { RecoveryFlow } from '@atls/next-identity-integration'

import React            from 'react'

import { RecoveryForm } from '@identity/auth-forms-fragment'
import { Background }   from '@ui/background'

const RecoveryPage = () => (
  <Background color='lightGreyForty' fill justifyContent='center' alignItems='center'>
    <RecoveryFlow returnToUrl='/auth/login'>
      <RecoveryForm />
    </RecoveryFlow>
  </Background>
)

export default RecoveryPage
