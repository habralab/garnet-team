import { SettingsFlow }            from '@atls/next-identity-integration'

import React                       from 'react'

import { RecoveryNewPasswordForm } from '@identity/auth-forms-fragment'
import { Header }                  from '@identity/header-fragment'
import { Background }              from '@ui/background'
import { Column }                  from '@ui/layout'

const RecoveryNewPasswordPage = () => (
  <Column fill>
    <Header />
    <Background fill color='lightGreyForty' justifyContent='center' alignItems='center'>
      <SettingsFlow>
        <RecoveryNewPasswordForm />
      </SettingsFlow>
    </Background>
  </Column>
)

export default RecoveryNewPasswordPage
