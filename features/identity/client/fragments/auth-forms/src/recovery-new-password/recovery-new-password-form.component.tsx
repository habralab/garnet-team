import { useFlow }           from '@fork/identity-integration'
import { FlowMessages }      from '@fork/identity-integration'
import { LocalizedMessages } from '@identity/messages-fragment'
import { checkPassword }     from '@utils/helpers/src'
import React                 from 'react'
import { FormattedMessage }  from 'react-intl'
import { useState }          from 'react'
import { useIntl }           from 'react-intl'

import { FlowSubmit } from '@fork/identity-integration'
import { FlowNode }   from '@fork/identity-integration'
import { Background } from '@ui/background'
import { Button }     from '@ui/button'
import { EyeIcon }    from '@ui/icon'
import { Input }      from '@ui/input'
import { Row }        from '@ui/layout'
import { Column }     from '@ui/layout'
import { Layout }     from '@ui/layout'
import { Text }       from '@ui/text'
import { Condition }  from '@ui/condition'

export const RecoveryNewPasswordForm = () => {
  const [showPassword, setShowPassword] = useState<boolean>(false)
  const [showPasswordRepeat, setShowPasswordRepeat] = useState<boolean>(false)
  const [password, setPassword] = useState<string>('')
  const [repeatPassword, setRepeatPassword] = useState<string>('')

  const { formatMessage } = useIntl()

  const { loading } = useFlow()

  const togglePassword = () => setShowPassword(!showPassword)
  const togglePasswordRepeat = () => setShowPasswordRepeat(!showPasswordRepeat)

  const arePasswordsIdentical = checkPassword(password, repeatPassword)

  return (
    <Background color='white' borderRadius='medium' boxShadow='black'>
      <Column paddingX={32} width={342} boxSizing='content-box' alignItems='center'>
        <Layout flexBasis={56} flexShrink={0}/>
        <Layout justifyContent='center'>
          <Text fontSize='preLarge' width='min-content' textAlign='center'>
            <FormattedMessage id={'recovery-new-password.title'}/>
          </Text>
        </Layout>
        <Layout flexBasis={40} flexShrink={0}/>
        <FlowNode name='password'>
          {(node, value, onChange) => (
            <Input
              {...node.attributes}
              placeholder={formatMessage({ id: 'recovery-new-password.password' })}
              value={value}
              onChange={(value) => {
                onChange(value)
                setPassword(value)
              }}
              iconSvg={<EyeIcon/>}
              valueWidth={16}
              valueHeight={16}
              errorText={node.messages.length > 0 ?
                <LocalizedMessages messages={node.messages}/> : undefined}
              onIconClick={togglePassword}
              type={showPassword ? 'text' : 'password'}
            />
          )}
        </FlowNode>
        <Layout flexBasis={40} flexShrink={0}/>
        <Condition match={!loading}>
          <Input
            placeholder={formatMessage({ id: 'recovery-new-password.repeat_password' })}
            onIconClick={togglePasswordRepeat}
            iconSvg={<EyeIcon/>}
            valueWidth={16}
            valueHeight={16}
            onChange={setRepeatPassword}
            type={showPasswordRepeat ? 'text' : 'password'}
          />
          <Layout flexBasis={40} flexShrink={0}/>
        </Condition>
        <FlowSubmit>
          {({ submitting, onSubmit }) => (
            <Row justifyContent='center'>
              <Button disabled={submitting || loading || !arePasswordsIdentical}
                      onClick={() => onSubmit({ method: 'password' })}>
                <FormattedMessage id={'recovery-new-password.save'}/>
              </Button>
            </Row>
          )}
        </FlowSubmit>
        <FlowMessages>
          {(messages) => (
            <LocalizedMessages messages={messages}/>
          )}
        </FlowMessages>
        <Layout flexBasis={56} flexShrink={0}/>
      </Column>
    </Background>
  )
}
