import { RecoveryFlow } from '@fork/identity-integration'
import { RecoveryForm } from '@identity/auth-forms-fragment/src'
import React            from 'react'
import { Background }   from '@ui/background'

const RecoveryPage = () => (
  <Background color='lightGreyForty' fill justifyContent='center' alignItems='center'>
    <RecoveryFlow returnToUrl='/auth/login'>
      <RecoveryForm/>
    </RecoveryFlow>
  </Background>
)

export default RecoveryPage
