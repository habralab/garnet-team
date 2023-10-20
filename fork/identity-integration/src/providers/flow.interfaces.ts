import type { RegistrationFlow }           from '@ory/kratos-client'
import type { VerificationFlow }           from '@ory/kratos-client'
import type { RecoveryFlow }               from '@ory/kratos-client'
import type { SettingsFlow }               from '@ory/kratos-client'
import type { LoginFlow }                  from '@ory/kratos-client'
import type { UpdateRegistrationFlowBody } from '@ory/kratos-client'
import type { UpdateVerificationFlowBody } from '@ory/kratos-client'
import type { UpdateRecoveryFlowBody }     from '@ory/kratos-client'
import type { UpdateSettingsFlowBody }     from '@ory/kratos-client'
import type { UpdateLoginFlowBody }        from '@ory/kratos-client'

export type FlowName = 'login' | 'registration' | 'recovery' | 'settings' | 'verification' | 'error'

export type Flow = RegistrationFlow | VerificationFlow | RecoveryFlow | SettingsFlow | LoginFlow

export type Body =
  | UpdateRegistrationFlowBody
  | UpdateVerificationFlowBody
  | UpdateRecoveryFlowBody
  | UpdateSettingsFlowBody
  | UpdateLoginFlowBody
