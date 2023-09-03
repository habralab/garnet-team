import React                from 'react'
import { FormattedMessage } from 'react-intl'
import { useMemo }          from 'react'
import { useState }         from 'react'
import { useIntl }          from 'react-intl'

import { FlowNode }         from '@fork/next-identity-integration'
import { FlowSubmit }       from '@fork/next-identity-integration'
import { TooltipMessages }  from '@identity/messages-fragment'
import { Button }           from '@ui/button'
import { Condition }        from '@ui/condition'
import { DoneAllIcon }      from '@ui/icons'
import { HideIcon }         from '@ui/icons'
import { ShowIcon }         from '@ui/icons'
import { Input }            from '@ui/input'
import { Box }              from '@ui/layout'
import { Column }           from '@ui/layout'
import { Row }              from '@ui/layout'
import { Layout }           from '@ui/layout'
import { Text }             from '@ui/text'
import { Tooltip }          from '@ui/tooltip'
import { useFlowNode }      from '@fork/next-identity-integration'
import { useFlow }          from '@fork/next-identity-integration'
import { isValidEmail }     from '@shared/utils'

const Form = () => {
  const emailNode = useFlowNode('traits.email')
  const passwordNode = useFlowNode('password')
  const { flow } = useFlow()
  const flowMessages = useMemo(() => flow?.ui?.messages || [], [flow])

  const [email, setEmail] = useState<string>('')
  const [password, setPassword] = useState<string>('')
  const [disabled, setDisabled] = useState<boolean>(true)
  const [showPassword, setShowPassword] = useState<boolean>(false)
  const { formatMessage } = useIntl()

  const isEmailLong = email.length > 5
  const isEmailValid = isValidEmail(email)
  const isPasswordValid = password.length > 3
  const isFormValid = isEmailValid && isPasswordValid

  useMemo(() => {
    if (isFormValid) {
      setDisabled(false)
    } else {
      setDisabled(true)
    }
    // eslint-disable-next-line
  }, [email, password])

  return (
    <Box flexDirection='column' width={['100%', 400, 400]}>
      <Box position='relative'>
        <FlowNode name='identifier'>
          {(field, value, onChange) => (
            <Tooltip
              isOpen={(emailNode && emailNode.messages.length > 0) || flowMessages.length > 0}
              container={
                <TooltipMessages messages={flowMessages.concat(emailNode?.messages ?? [])} />
              }
            >
              <Column fill>
                <Layout>
                  <Input
                    {...field.attributes}
                    value={value}
                    onChange={(e) => {
                      onChange(e)
                      setEmail(e)
                    }}
                    fieldIsFilled={isEmailValid}
                    placeholder={formatMessage({
                      id: 'app_login_password.email',
                      defaultMessage: 'Электронная почта',
                    })}
                  />
                </Layout>
              </Column>
            </Tooltip>
          )}
        </FlowNode>
        <Box position='absolute' top={15} right={16}>
          <Condition match={isEmailValid}>
            <DoneAllIcon width={18} height={10} />
          </Condition>
        </Box>
      </Box>
      <Layout flexBasis={16} flexShrink={0} />
      <Condition match={isEmailLong}>
        <Box position='relative'>
          <FlowNode name='password'>
            {(field, value, onChange) => (
              <Tooltip
                isOpen={passwordNode && passwordNode.messages.length > 0}
                container={<TooltipMessages messages={passwordNode?.messages} />}
              >
                <Column fill>
                  <Layout>
                    <Input
                      {...field.attributes}
                      value={value}
                      onChange={(e) => {
                        onChange(e)
                        setPassword(e)
                      }}
                      fieldIsFilled={isPasswordValid}
                      type={showPassword ? 'text' : 'password'}
                      placeholder={formatMessage({
                        id: 'app_login_password.password',
                        defaultMessage: 'Пароль',
                      })}
                    />
                  </Layout>
                </Column>
              </Tooltip>
            )}
          </FlowNode>
          <Box
            position='absolute'
            top={10}
            right={16}
            onClick={() => (showPassword ? setShowPassword(false) : setShowPassword(true))}
            style={{ cursor: 'pointer' }}
          >
            <Condition match={showPassword}>
              <HideIcon width={20} height={20} />
            </Condition>
            <Condition match={!showPassword}>
              <ShowIcon width={20} height={20} />
            </Condition>
          </Box>
        </Box>
        <Layout flexBasis={16} flexShrink={0} />
      </Condition>
      <Row>
        <FlowSubmit>
          {({ submitting, onSubmit }) => (
            <Button
              size='withoutPaddingMiddlingHeight'
              variant='redBackground'
              fill
              disabled={submitting || disabled}
              onClick={() => onSubmit({ method: 'password' })}
            >
              <Text fontWeight='medium' fontSize='medium'>
                <Condition match={submitting || disabled}>
                  <FormattedMessage id='app_login_password.continue' defaultMessage='Продолжить' />
                </Condition>
                <Condition match={!submitting && !disabled}>
                  <FormattedMessage id='app_login_password.login' defaultMessage='Войти' />
                </Condition>
              </Text>
            </Button>
          )}
        </FlowSubmit>
      </Row>
    </Box>
  )
}

export { Form }
