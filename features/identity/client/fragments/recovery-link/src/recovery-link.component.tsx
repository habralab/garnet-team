/* eslint-disable jsx-a11y/label-has-associated-control */

import { Button }           from '@atls-ui-proto/button'
import { Input }            from '@atls-ui-proto/input'
import { Column }           from '@atls-ui-proto/layout'
import { Layout }           from '@atls-ui-proto/layout'
import { Text }             from '@atls-ui-proto/text'

import React                from 'react'
import { FormattedMessage } from 'react-intl'

import { FlowNode }         from '@fork/next-identity-integration'
import { FlowSubmit }       from '@fork/next-identity-integration'
import { FlowMessages }     from '@fork/next-identity-integration'
import { FlowNodeMessages } from '@fork/next-identity-integration'
import { FieldMessages }    from '@identity/messages-fragment'
import { GeneralMessages }  from '@identity/messages-fragment'

export const RecoveryLink = () => (
  <Column justifyContent='center' alignItems='center'>
    <Layout maxWidth={320} width='100%'>
      <Column>
        <FlowMessages>
          {(messages) => (
            <Layout>
              <GeneralMessages messages={messages} />
            </Layout>
          )}
        </FlowMessages>
        <Layout flexBasis={20} />
        <Layout>
          <Text fontWeight={600} fontSize={12}>
            <label htmlFor='email'>
              <FormattedMessage id='recovery_link.email' defaultMessage='Email' />
            </label>
          </Text>
        </Layout>
        <Layout mt='8px'>
          <FlowNode name='email'>
            {({ attributes }, value, onChange) => (
              <FormattedMessage
                id='recovery_link.email_address'
                defaultMessage='Введите адрес вашей почты'
              >
                {(msg) => (
                  <Input
                    placeholder={String(msg)}
                    {...attributes}
                    value={value}
                    onChange={onChange}
                  />
                )}
              </FormattedMessage>
            )}
          </FlowNode>
        </Layout>
        <Layout pb='8px'>
          <FlowNodeMessages name='email'>
            {(messages) => <FieldMessages messages={messages} />}
          </FlowNodeMessages>
        </Layout>
        <Layout>
          <Text>
            <FormattedMessage
              id='recovery_link.enter_email'
              defaultMessage='Введите email указанный при регистрации'
            />
          </Text>
        </Layout>
        <Layout flexBasis={40} />
        <Layout>
          <FlowSubmit>
            {({ submitting, onSubmit }) => (
              <Button
                type='submit'
                size='large'
                fill
                disabled={submitting}
                onClick={() => onSubmit({ method: 'link' })}
              >
                <FormattedMessage
                  id='recovery_link.reset_password'
                  defaultMessage='Сбросить пароль'
                />
              </Button>
            )}
          </FlowSubmit>
        </Layout>
      </Column>
    </Layout>
  </Column>
)
