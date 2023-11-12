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
import { EyeIcon }           from '@ui/icon'
import { Input }             from '@ui/input'
import { Row }               from '@ui/layout'
import { Layout }            from '@ui/layout'
import { Column }            from '@ui/layout'
import { NextLink }          from '@ui/link'
import { Text }              from '@ui/text'

export const LoginForm = () => {
  const [showPassword, setShowPassword] = useState<boolean>(false)

  const { formatMessage } = useIntl()
  const { loading, flow } = useFlow()

  const togglePassword = () => setShowPassword(!showPassword)

  return (
    <Background color='white' borderRadius='medium' boxShadow='black'>
      <Column paddingX={32} width={342} boxSizing='content-box' alignItems='center'>
        <Layout flexBasis={56} flexShrink={0} />
        <Layout justifyContent='center'>
          <Text fontSize='preLarge' width='min-content' textAlign='center'>
            <FormattedMessage id='login_password.log_in' />
          </Text>
        </Layout>
        <Layout flexBasis={40} flexShrink={0} />
        <FlowNode name='identifier'>
          {(node, value, onChange) => (
            <Input
              {...node.attributes}
              placeholder={formatMessage({ id: 'login_password.email' })}
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
        <Layout flexBasis={16} flexShrink={0} />
        <FlowNode name='password'>
          {(node, value, onChange) => (
            <Input
              {...node.attributes}
              placeholder={formatMessage({ id: 'login_password.password' })}
              value={value}
              onChange={onChange}
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
        <FlowSubmit>
          {({ submitting, onSubmit }) => (
            <Row justifyContent='center'>
              <Button
                disabled={submitting || loading || !flow}
                onClick={() => onSubmit({ method: 'password' })}
              >
                <FormattedMessage id='login_password.log_in' />
              </Button>
            </Row>
          )}
        </FlowSubmit>
        <Layout flexBasis={8} />
        <Row justifyContent='space-between'>
          <Layout>
            <FlowMessages>{(messages) => <LocalizedMessages messages={messages} />}</FlowMessages>
          </Layout>
          <NextLink fontSize='normal' path='/auth/recovery' href='/auth/recovery'>
            <FormattedMessage id='login_password.forgot' />
          </NextLink>
        </Row>
        <Layout flexBasis={56} flexShrink={0} />
      </Column>
    </Background>
  )
}
