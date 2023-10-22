import { FlowMessages }      from '@atls/next-identity-integration'
import { FlowSubmit }        from '@atls/next-identity-integration'
import { FlowNode }          from '@atls/next-identity-integration'
import { useFlow }           from '@atls/next-identity-integration'

import React                 from 'react'
import { FormattedMessage }  from 'react-intl'
import { useState }          from 'react'
import { useIntl }           from 'react-intl'

import { LocalizedMessages } from '@identity/messages-fragment'
import { Background }        from '@ui/background'
import { Button }            from '@ui/button'
import { Condition }         from '@ui/condition'
import { EyeIcon }           from '@ui/icon'
import { Input }             from '@ui/input'
import { Row }               from '@ui/layout'
import { Column }            from '@ui/layout'
import { Layout }            from '@ui/layout'
import { Text }              from '@ui/text'
import { checkPassword }     from '@utils/helpers'

export const RegistrationForm = () => {
  const [showPassword, setShowPassword] = useState<boolean>(false)
  const [showPasswordRepeat, setShowPasswordRepeat] = useState<boolean>(false)
  const [password, setPassword] = useState<string>('')
  const [repeatPassword, setRepeatPassword] = useState<string>('')

  const { formatMessage } = useIntl()

  const { loading, flow } = useFlow()

  const togglePassword = () => setShowPassword(!showPassword)
  const togglePasswordRepeat = () => setShowPasswordRepeat(!showPasswordRepeat)

  const arePasswordsIdentical = checkPassword(password, repeatPassword)

  return (
    <Background color='white' borderRadius='medium' boxShadow='black'>
      <Column paddingX={32} width={342} boxSizing='content-box' alignItems='center'>
        <Layout flexBasis={56} flexShrink={0} />
        <Layout justifyContent='center'>
          <Text fontSize='preLarge' width='min-content' textAlign='center'>
            <FormattedMessage id='registration_password.title' />
          </Text>
        </Layout>
        <Layout flexBasis={40} flexShrink={0} />
        <FlowNode name='traits.name.first'>
          {(node, value, callback) => (
            <Input
              {...node.attributes}
              value={value}
              onChange={callback}
              errorText={
                node.messages.length > 0 ? (
                  <LocalizedMessages messages={node.messages} />
                ) : undefined
              }
              placeholder={formatMessage({ id: 'registration_password.name' })}
            />
          )}
        </FlowNode>

        <Layout flexBasis={16} flexShrink={0} />
        <FlowNode name='traits.name.last'>
          {(node, value, callback) => (
            <Input
              {...node.attributes}
              value={value}
              onChange={callback}
              errorText={
                node.messages.length > 0 ? (
                  <LocalizedMessages messages={node.messages} />
                ) : undefined
              }
              placeholder={formatMessage({ id: 'registration_password.last_name' })}
            />
          )}
        </FlowNode>
        <Layout flexBasis={40} flexShrink={0} />
        <FlowNode name='traits.email'>
          {(node, value, onChange) => (
            <Input
              {...node.attributes}
              placeholder={formatMessage({ id: 'registration_password.email' })}
              value={value}
              errorText={
                node.messages.length > 0 ? (
                  <LocalizedMessages messages={node.messages} />
                ) : undefined
              }
              onChange={onChange}
            />
          )}
        </FlowNode>
        <Layout flexBasis={40} flexShrink={0} />
        <FlowNode name='password'>
          {(node, value, onChange) => (
            <Input
              {...node.attributes}
              placeholder={formatMessage({ id: 'registration_password.password' })}
              value={value}
              onChange={(newValue) => {
                onChange(newValue)
                setPassword(newValue)
              }}
              iconSvg={<EyeIcon />}
              valueWidth={16}
              valueHeight={16}
              errorText={
                node.messages.length > 0 ? (
                  <LocalizedMessages messages={node.messages} />
                ) : undefined
              }
              onIconClick={togglePassword}
              type={showPassword ? 'text' : 'password'}
            />
          )}
        </FlowNode>
        <Layout flexBasis={40} flexShrink={0} />
        <Condition match={Boolean(flow)}>
          <Input
            placeholder={formatMessage({ id: 'registration_password.repeat_password' })}
            onIconClick={togglePasswordRepeat}
            iconSvg={<EyeIcon />}
            valueWidth={16}
            valueHeight={16}
            onChange={setRepeatPassword}
            type={showPasswordRepeat ? 'text' : 'password'}
          />
          <Layout flexBasis={40} flexShrink={0} />
        </Condition>
        <FlowSubmit>
          {({ submitting, onSubmit }) => (
            <Row justifyContent='center'>
              <Button
                disabled={submitting || loading || !arePasswordsIdentical || !flow}
                onClick={() => onSubmit()}
              >
                <FormattedMessage id='registration_password.register' />
              </Button>
            </Row>
          )}
        </FlowSubmit>
        <FlowMessages>{(messages) => <LocalizedMessages messages={messages} />}</FlowMessages>
        <Layout flexBasis={56} flexShrink={0} />
      </Column>
    </Background>
  )
}
