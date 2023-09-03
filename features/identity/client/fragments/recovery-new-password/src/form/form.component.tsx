import React                from 'react'
import { FormattedMessage } from 'react-intl'
import { useMemo }          from 'react'
import { useState }         from 'react'
import { useIntl }          from 'react-intl'

import { FlowNode }         from '@fork/next-identity-integration'
import { FlowSubmit }       from '@fork/next-identity-integration'
import { FlowNodeMessages } from '@fork/next-identity-integration'
import { FlowMessages }     from '@fork/next-identity-integration'
import { FieldMessages }    from '@identity/messages-fragment'
import { GeneralMessages }  from '@identity/messages-fragment'
import { Button }           from '@ui/button'
import { Condition }        from '@ui/condition'
import { HideIcon }         from '@ui/icons'
import { ShowIcon }         from '@ui/icons'
import { Input }            from '@ui/input'
import { Box }              from '@ui/layout'
import { Column }           from '@ui/layout'
import { Row }              from '@ui/layout'
import { Layout }           from '@ui/layout'
import { Text }             from '@ui/text'

import { FormProps }        from './form.interfaces'

const doNothing = () => {
  // do nothing
}

const Form = ({ onSuccess = doNothing, onFailure = doNothing }: FormProps) => {
  const [disabled, setDisabled] = useState<boolean>(true)
  const [password, setPassword] = useState<string>('')
  const [repeatPassword, setRepeatPassword] = useState<string>('')
  const [showPassword, setShowPassword] = useState<boolean>(false)
  const [showRepeatPassword, setShowRepeatPassword] = useState<boolean>(false)
  const { formatMessage } = useIntl()

  const isPasswordLong = password.length > 3
  const isPasswordValid = password.length > 7
  const isRepeatPasswordValid = password !== '' && password === repeatPassword

  const isFormValid = isRepeatPasswordValid

  useMemo(() => {
    if (isFormValid) {
      setDisabled(false)
    } else {
      setDisabled(true)
    }
    // eslint-disable-next-line
  }, [password, repeatPassword])

  return (
    <Box flexDirection='column' width={['100%', 400, 400]}>
      <Box position='relative'>
        <FlowNode name='password'>
          {(field, value, onChange) => (
            <Column fill>
              <Layout>
                <Input
                  {...field.attributes}
                  id='password'
                  value={value}
                  onChange={(e) => {
                    onChange(e)
                    setPassword(e)
                  }}
                  fieldIsFilled={isPasswordValid}
                  type={showPassword ? 'text' : 'password'}
                  placeholder={formatMessage({
                    id: 'app_registration.create_password',
                    defaultMessage: 'Придумайте пароль',
                  })}
                />
              </Layout>
              <Layout flexBasis={24} pb='8px'>
                <FlowNodeMessages name='password'>
                  {(messages) => <FieldMessages messages={messages} />}
                </FlowNodeMessages>
              </Layout>
            </Column>
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
      <Condition match={isPasswordLong}>
        <Box position='relative'>
          <Input
            value={repeatPassword}
            onChange={setRepeatPassword}
            fieldIsFilled={isRepeatPasswordValid}
            type={showRepeatPassword ? 'text' : 'password'}
            placeholder={formatMessage({
              id: 'app_registration.repeat_password',
              defaultMessage: 'Подтвердите пароль',
            })}
          />
          <Box
            position='absolute'
            top={10}
            right={16}
            onClick={() =>
              showRepeatPassword ? setShowRepeatPassword(false) : setShowRepeatPassword(true)
            }
            style={{ cursor: 'pointer' }}
          >
            <Condition match={showRepeatPassword}>
              <HideIcon width={20} height={20} />
            </Condition>
            <Condition match={!showRepeatPassword}>
              <ShowIcon width={20} height={20} />
            </Condition>
          </Box>
        </Box>
      </Condition>
      <Row>
        <FlowSubmit>
          {({ submitting, onSubmit }) => (
            <Button
              size='withoutPaddingMiddlingHeight'
              variant='redBackground'
              disabled={submitting || disabled}
              onClick={() => {
                onSubmit({
                  method: 'password',
                })
              }}
              fill
            >
              <Text fontWeight='medium' fontSize='medium'>
                <Condition match={submitting || disabled}>
                  <FormattedMessage id='app_registration.continue' defaultMessage='Продолжить' />
                </Condition>
                <Condition match={!submitting && !disabled}>
                  <FormattedMessage
                    id='app_registration.registration'
                    defaultMessage='Сохранить и войти'
                  />
                </Condition>
              </Text>
            </Button>
          )}
        </FlowSubmit>
      </Row>
      <FlowMessages>{(messages) => <GeneralMessages messages={messages} />}</FlowMessages>
    </Box>
  )
}

export { Form }
