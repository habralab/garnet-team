import { SettingsFlow }            from '@atls/next-identity-integration'

import React                       from 'react'

import { RecoveryNewPasswordForm } from '@identity/auth-forms-fragment'
import { Background }              from '@ui/background'

const RecoveryNewPasswordPage = () => (
  <SettingsFlow>
    <Background fill color='lightGreyForty' justifyContent='center' alignItems='center'>
      <RecoveryNewPasswordForm />
    </Background>
  </SettingsFlow>
)

export default RecoveryNewPasswordPage
