import React                from 'react'
import { useState }         from 'react'

import { RecoveryFlow }     from '@fork/next-identity-integration'
import { RecoveryPassword } from '@identity/recovery-password-fragment'
import { Complete }         from '@identity/recovery-password-fragment'
import { Condition }        from '@ui/condition/src'

export const RecoveryPage = () => {
  const [isComplete, setIsComplete] = useState(false)

  const setComplete = () => setIsComplete(true)

  return (
    <RecoveryFlow>
      <Condition match={!isComplete}>
        <RecoveryPassword setComplete={setComplete} />
      </Condition>
      <Condition match={isComplete}>
        <Complete />
      </Condition>
    </RecoveryFlow>
  )
}
