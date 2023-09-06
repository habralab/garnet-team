/* eslint-disable jsx-a11y/label-has-associated-control */

import React                from 'react'
import { FormattedMessage } from 'react-intl'

import { FlowNode }         from '@fork/next-identity-integration'
import { FlowSubmit }       from '@fork/next-identity-integration'
import { FlowNodeMessages } from '@fork/next-identity-integration'
import { FlowNodesFilter }  from '@fork/next-identity-integration'
import { FieldMessages }    from '@identity/messages-fragment'
import { Button }           from '@ui/button'
import { Input }            from '@ui/input'
import { Column }           from '@ui/layout'
import { Row }              from '@ui/layout'
import { Layout }           from '@ui/layout'
import { Text }             from '@ui/text'

export const ProfileSecurity = () => (
  <Column height='100%' maxWidth={['none', 'none', 520]}>
    <Layout>
      <Text fontSize={28} fontWeigth={500}>
        <FormattedMessage id='profile_security.security' defaultMessage='Безопасность' />
      </Text>
    </Layout>
    <Layout flexBasis={24} />
    <Layout>
      <Text fontWeight={600} fontSize={12}>
        <label htmlFor='password'>
          <FormattedMessage id='profile_security.password' defaultMessage='Пароль' />
        </label>
      </Text>
    </Layout>
    <Layout flexBasis={10} />
    <Layout>
      <FlowNode name='password'>
        {({ attributes }, value, onChange) => (
          <Input fieldIsFilled={value > 3} {...attributes} value={value} onChange={onChange} />
        )}
      </FlowNode>
    </Layout>
    <Layout flexBasis={24} pb='8px'>
      <FlowNodeMessages name='password'>
        {(messages) => <FieldMessages messages={messages} />}
      </FlowNodeMessages>
    </Layout>

    <Layout>
      <Row flexDirection={['column', 'column', 'row']}>
        <Layout flexBasis={['auto', 'auto', 120]}>
          <FlowSubmit>
            {({ submitting, onSubmit }) => (
              <Button
                type='submit'
                rounding={6}
                fill
                disabled={submitting}
                onClick={() => onSubmit({ method: 'password' })}
              >
                <FormattedMessage id='profile_security.save' defaultMessage='Сохранить' />
              </Button>
            )}
          </FlowSubmit>
        </Layout>
      </Row>
    </Layout>

    <Layout flexBasis={40} />

    <Layout>
      <FlowNodesFilter predicate={(node) => node.group === 'totp' && node.type === 'img'}>
        <Column>
          <Layout>
            <Text fontSize={18} fontWeigth={500}>
              <FormattedMessage
                id='profile_security.manage_totp'
                defaultMessage='Настройка 2FA TOTP Authenticator App'
              />
            </Text>
          </Layout>

          <Layout flexBasis={24} />

          <Layout>
            <Text fontSize={14} fontWeigth={500}>
              <FormattedMessage
                id='profile_security.totp_app'
                defaultMessage='Добавьте TOTP Authenticator App в свою учетную запись, чтобы улучшить её безопасность. популярные приложения для аутентификации — LastPass и Google Authenticator'
              />
            </Text>
          </Layout>
          <Layout flexBasis={10} />
          <Layout>
            <FlowNode name='totp_qr'>
              {({ attributes }) => <img {...attributes} alt='totp_qr' />}
            </FlowNode>
          </Layout>
          <Layout flexBasis={24} />
          <Layout>
            <Text fontSize={14} fontWeigth={500}>
              <label htmlFor='totp_secret'>
                <FormattedMessage
                  id='profile_security.totp_secret'
                  defaultMessage='Это секрет вашего приложения для проверки подлинности. Используйте его, если вы не можете отсканировать QR-код.'
                />
              </label>
            </Text>
          </Layout>

          <Layout flexBasis={16} />

          <Layout>
            <FlowNode name='totp_secret_key'>
              {({ attributes }) => (
                <Text fontSize={24} fontWeight={600}>
                  {attributes.text.text}
                </Text>
              )}
            </FlowNode>
          </Layout>

          <Layout flexBasis={24} />
          <Layout>
            <Text fontWeight={600} fontSize={12}>
              <label htmlFor='totp_code'>
                <FormattedMessage
                  id='profile_security.totp_code'
                  defaultMessage='Код верификации'
                />
              </label>
            </Text>
          </Layout>

          <Layout flexBasis={10} />
          <Layout>
            <FlowNode name='totp_code'>
              {({ attributes }, value, onChange) => (
                <Input
                  fieldIsFilled={value > 0}
                  {...attributes}
                  value={value}
                  onChange={onChange}
                />
              )}
            </FlowNode>
          </Layout>
          <Layout flexBasis={24} pb='8px'>
            <FlowNodeMessages name='totp_code'>
              {(messages) => <FieldMessages messages={messages} />}
            </FlowNodeMessages>
          </Layout>

          <Layout>
            <Row flexDirection={['column', 'column', 'row']}>
              <Layout flexBasis={['auto', 'auto', 120]}>
                <FlowSubmit>
                  {({ submitting, onSubmit }) => (
                    <Button
                      type='submit'
                      rounding={6}
                      fill
                      disabled={submitting}
                      onClick={() => onSubmit({ method: 'totp' })}
                    >
                      <FormattedMessage id='profile_security.save' defaultMessage='Сохранить' />
                    </Button>
                  )}
                </FlowSubmit>
              </Layout>
            </Row>
          </Layout>
        </Column>
      </FlowNodesFilter>
    </Layout>

    <Layout>
      <FlowNodesFilter
        predicate={(node) => node.group === 'totp' && node.attributes.name === 'totp_unlink'}
      >
        <Column>
          <Layout>
            <Text fontSize={18} fontWeigth={500}>
              <FormattedMessage
                id='profile_security.unlink_totp'
                defaultMessage='Отключить 2FA TOTP Authenticator App'
              />
            </Text>
          </Layout>

          <Layout flexBasis={24} />
          <Layout>
            <Row flexDirection={['column', 'column', 'row']}>
              <Layout flexBasis={['auto', 'auto', 120]}>
                <FlowSubmit>
                  {({ submitting, onSubmit }) => (
                    <Button
                      type='submit'
                      rounding={6}
                      fill
                      disabled={submitting}
                      onClick={() => onSubmit({ totp_unlink: true, method: undefined })}
                    >
                      <FormattedMessage
                        id='profile_security.disable_totp'
                        defaultMessage='Отключить'
                      />
                    </Button>
                  )}
                </FlowSubmit>
              </Layout>
            </Row>
          </Layout>
        </Column>
      </FlowNodesFilter>
    </Layout>
  </Column>
)
