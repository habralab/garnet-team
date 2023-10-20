import { SettingsFlow }            from '@fork/identity-integration/src'
import { RecoveryNewPasswordForm } from '@identity/auth-forms-fragment'
import { Background }              from '@ui/background/src'
import React                       from 'react'

const RecoveryNewPasswordPage = () =>
  <SettingsFlow>
    <Background fill color='lightGreyForty' justifyContent='center' alignItems='center'>
      <RecoveryNewPasswordForm/>
    </Background>
  </SettingsFlow>

export default RecoveryNewPasswordPage
