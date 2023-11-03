import { RecoveryFlow } from '@atls/next-identity-integration'

import React            from 'react'

import { RecoveryForm } from '@identity/auth-forms-fragment'
import { Header }       from '@identity/header-fragment'
import { Background }   from '@ui/background'
import { Column }       from '@ui/layout'

const RecoveryPage = () => (
  <Column fill>
    <Header />
    <Background color='lightGreyForty' fill justifyContent='center' alignItems='center'>
      <RecoveryFlow returnToUrl='/auth/login'>
        <RecoveryForm />
      </RecoveryFlow>
    </Background>
  </Column>
)

export default RecoveryPage
