import React                    from 'react'
import { useState }             from 'react'

import { RegistrationFlow }     from '@fork/next-identity-integration'
import { Complete }             from '@identity/registration-password-fragment'
import { RegistrationPassword } from '@identity/registration-password-fragment'
import { Condition }            from '@ui/condition/src'

export const RegistrationPage = () => {
  const [isComplete, setIsComplete] = useState(false)

  const setComplete = () => setIsComplete(true)

  return (
    <RegistrationFlow>
      <Condition match={!isComplete}>
        <RegistrationPassword setComplete={setComplete} />
      </Condition>
      <Condition match={isComplete}>
        <Complete />
      </Condition>
    </RegistrationFlow>
  )
}
