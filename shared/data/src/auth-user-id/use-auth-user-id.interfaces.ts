import { Session } from '@ory/kratos-client'

export type UseAuthUserIdHook = () => { session?: Session; authUserId?: string }
