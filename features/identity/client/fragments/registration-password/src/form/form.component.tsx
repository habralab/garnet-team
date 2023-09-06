import React                from 'react'
import { FormattedMessage } from 'react-intl'
import { useMemo }          from 'react'
import { useState }         from 'react'
import { useEffect }        from 'react'
import { useIntl }          from 'react-intl'

import { FlowNode }         from '@fork/next-identity-integration'
import { FlowSubmit }       from '@fork/next-identity-integration'
import { TooltipMessages }  from '@identity/messages-fragment'
import { Button }           from '@ui/button'
import { Condition }        from '@ui/condition'
import { CheckCheckIcon }   from '@ui/icons'
import { HideIcon }         from '@ui/icons'
import { InfoIcon }         from '@ui/icons'
import { ShowIcon }         from '@ui/icons'
import { Input }            from '@ui/input'
import { Box }              from '@ui/layout'
import { Column }           from '@ui/layout'
import { Row }              from '@ui/layout'
import { Layout }           from '@ui/layout'
import { NextLink }         from '@ui/link'
import { Space }            from '@ui/text'
import { Text }             from '@ui/text'
import { Tooltip }          from '@ui/tooltip'
import { useFlow }          from '@fork/next-identity-integration'
import { useFlowNode }      from '@fork/next-identity-integration'
import { isValidEmail }     from '@shared/utils'

import { FormProps }        from './form.interfaces'

const Form = ({ showPromo, setComplete }: FormProps) => {
  const { isValid, flow } = useFlow()
  const flowMessages = useMemo(() => flow?.ui.messages || [], [flow])

  const emailNode = useFlowNode('traits.email')
  const passwordNode = useFlowNode('password')
  const firstNameNode = useFlowNode('traits.name.first')
  const lastNameNode = useFlowNode('traits.name.last')

  useEffect(() => {
    if (isValid) setComplete()
    // eslint-disable-next-line
  }, [isValid])

  const [promo, setPromo] = useState<string>('')
  const [email, setEmail] = useState<string>('')
  const [password, setPassword] = useState<string>('')
  const [repeatPassword, setRepeatPassword] = useState<string>('')
  const [name, setName] = useState<string>('')
  const [disabled, setDisabled] = useState<boolean>(true)
  const [showPassword, setShowPassword] = useState<boolean>(false)
  const [showRepeatPassword, setShowRepeatPassword] = useState<boolean>(false)
  const { formatMessage } = useIntl()

  const nameRegexp = /^([A-ZА-ЯЁ][a-zа-яё]{2,})+\s+([A-ZА-ЯЁ][a-zа-яё]{2,})+$/

  const isPromoValid = promo.length > 1
  const isEmailLong = email.length > 5
  const isEmailValid = isValidEmail(email)
  const isPasswordLong = password.length > 3
  const isRepeatPasswordValid = password !== '' && password === repeatPassword
  const isNameValid = nameRegexp.test(name)
  const isFormValid = isEmailValid && isPasswordLong && isRepeatPasswordValid && isNameValid

  useMemo(() => {
    if (isFormValid) {
      setDisabled(false)
    } else {
      setDisabled(true)
    }
    // eslint-disable-next-line
  }, [email, password, repeatPassword, name])

  return (
    <Box flexDirection='column' width={['100%', 400, 400]}>
      <Condition match={showPromo}>
        <Box position='relative'>
          <Input
            value={promo}
            onChange={setPromo}
            fieldIsFilled={isNameValid}
            required={false}
            type='text'
            placeholder={formatMessage({
              id: 'app_registration.promo',
              defaultMessage: 'Промокод',
            })}
          />
          <Condition match={isPromoValid}>
            <Box position='absolute' top={15} right={16} style={{ cursor: 'pointer' }}>
              <CheckCheckIcon width={17} height={10} />
            </Box>
          </Condition>
          <Condition match={!isPromoValid}>
            <Box position='absolute' top={10} right={16} style={{ cursor: 'pointer' }}>
              <InfoIcon width={20} height={20} />
            </Box>
          </Condition>
        </Box>
        <Layout flexBasis={16} flexShrink={0} />
      </Condition>
      <Box>
        <FlowNode name='traits.email'>
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
                    id='traits.email'
                    value={value}
                    onChange={(e) => {
                      onChange(e)
                      setEmail(e)
                    }}
                    fieldIsFilled={isEmailValid}
                    placeholder={formatMessage({
                      id: 'app_registration.email',
                      defaultMessage: 'Электронная почта',
                    })}
                  />
                </Layout>
              </Column>
            </Tooltip>
          )}
        </FlowNode>
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
                      id='password'
                      value={value}
                      onChange={(e) => {
                        onChange(e)
                        setPassword(e)
                      }}
                      fieldIsFilled={isPasswordLong}
                      type={showPassword ? 'text' : 'password'}
                      placeholder={formatMessage({
                        id: 'app_registration.create_password',
                        defaultMessage: 'Придумайте пароль',
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
      </Condition>
      <Layout flexBasis={16} flexShrink={0} />
      <Condition match={isEmailLong && isPasswordLong}>
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
      <Layout flexBasis={16} flexShrink={0} />
      <Condition match={isEmailLong && isPasswordLong && isRepeatPasswordValid}>
        <Box>
          <Tooltip
            isOpen={
              (firstNameNode && firstNameNode.messages.length > 0) ||
              (lastNameNode && lastNameNode.messages.length > 0)
            }
            container={
              <TooltipMessages
                messages={firstNameNode?.messages.concat(lastNameNode?.messages || [])}
              />
            }
          >
            <Input
              value={name}
              onChange={setName}
              fieldIsFilled={isNameValid}
              type='text'
              placeholder={formatMessage({
                id: 'app_registration.name',
                defaultMessage: 'Имя Фамилия',
              })}
            />
          </Tooltip>
        </Box>
        <Layout flexBasis={8} flexShrink={0} />
        <Box width={300}>
          <Text fontWeight='semiMedium' fontSize='micro' lineHeight='medium' color='text.black'>
            <FormattedMessage
              id='app_registration.name_will_be_displayed'
              defaultMessage='Имя будет отображаться рядом с документами вашего авторства'
            />
          </Text>
        </Box>
        <Layout flexBasis={16} flexShrink={0} />
      </Condition>
      <Condition match={isFormValid}>
        <Box alignItems='end'>
          <Box>
            <Text
              display='inline'
              fontWeight='semiMedium'
              fontSize='micro'
              lineHeight='medium'
              color='text.black'
            >
              <FormattedMessage
                id='app_registration.by_clicking_you_accept'
                defaultMessage='Нажимая “Зарегистрироваться” вы принимаете'
              />
              <Space />
              <NextLink path='/' display='inline-flex'>
                <Text
                  fontWeight='semiMedium'
                  fontSize='micro'
                  lineHeight='medium'
                  color='text.black'
                  style={{ textDecoration: 'underline' }}
                >
                  <FormattedMessage
                    id='app_registration.privacy_policy'
                    defaultMessage='политику конфиденциальности'
                  />
                </Text>
              </NextLink>
            </Text>
          </Box>
        </Box>
        <Layout flexBasis={8} flexShrink={0} />
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
                  traits: {
                    email,
                    name: { first: name.split(' ')[0], last: name.split(' ')[1] },
                  },
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
                    defaultMessage='Зарегистрироваться'
                  />
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
