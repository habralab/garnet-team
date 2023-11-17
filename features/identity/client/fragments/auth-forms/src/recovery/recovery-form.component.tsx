import { FlowMessages }      from '@atls/next-identity-integration'
import { FlowSubmit }        from '@atls/next-identity-integration'
import { FlowNode }          from '@atls/next-identity-integration'
import { useFlow }           from '@atls/next-identity-integration'

import React                 from 'react'
import { FormattedMessage }  from 'react-intl'
import { useIntl }           from 'react-intl'

import { LocalizedMessages } from '@identity/messages-fragment'
import { Background }        from '@ui/background'
import { Button }            from '@ui/button'
import { Form }              from '@ui/form'
import { Input }             from '@ui/input'
import { Row }               from '@ui/layout'
import { Layout }            from '@ui/layout'
import { Column }            from '@ui/layout'
import { NextLink }          from '@ui/link'
import { Text }              from '@ui/text'

export const RecoveryForm = () => {
  const { formatMessage } = useIntl()

  const { loading, flow } = useFlow()

  return (
    <Background color='white' borderRadius='medium' boxShadow='black'>
      <Form>
        <Column paddingX={32} width={342} boxSizing='content-box' alignItems='center'>
          <Layout flexBasis={56} flexShrink={0} />
          <Layout justifyContent='center'>
            <Text fontSize='preLarge' width='min-content' textAlign='center'>
              <FormattedMessage id='recovery.title' />
            </Text>
          </Layout>
          <Layout flexBasis={40} flexShrink={0} />
          <FlowNode name='email'>
            {(node, value, onChange) => (
              <Input
                {...node.attributes}
                placeholder={formatMessage({ id: 'recovery.email' })}
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
          <FlowSubmit>
            {({ submitting, onSubmit }) => (
              <Row justifyContent='center'>
                <Button
                  disabled={submitting || loading || !flow}
                  onClick={() => onSubmit({ method: 'link' })}
                >
                  <FormattedMessage id='recovery.recover_access' />
                </Button>
              </Row>
            )}
          </FlowSubmit>
          <Layout flexBasis={8} />
          <Row justifyContent='space-between'>
            <Layout>
              <FlowMessages>{(messages) => <LocalizedMessages messages={messages} />}</FlowMessages>
            </Layout>
            <NextLink fontSize='normal' path='/auth/login'>
              <FormattedMessage id='recovery.remember_password' />
            </NextLink>
          </Row>
          <Layout flexBasis={56} flexShrink={0} />
        </Column>
      </Form>
    </Background>
  )
}
