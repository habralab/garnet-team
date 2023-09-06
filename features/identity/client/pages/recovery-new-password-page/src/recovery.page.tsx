import React                   from 'react'

import { SettingsFlow }        from '@fork/next-identity-integration'
import { RecoveryNewPassword } from '@identity/recovery-new-password-fragment'

export const RecoveryPage = () => (
  <SettingsFlow>
    <RecoveryNewPassword />
  </SettingsFlow>
)
