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
import { Input }            from '@ui/input'
import { Box }              from '@ui/layout'
import { Column }           from '@ui/layout'
import { Row }              from '@ui/layout'
import { Layout }           from '@ui/layout'
import { Text }             from '@ui/text'
import { Tooltip }          from '@ui/tooltip'
import { useFlow }          from '@fork/next-identity-integration'
import { useFlowNode }      from '@fork/next-identity-integration'
import { isValidEmail }     from '@shared/utils'

import { FormProps }        from './form.interfaces'

const Form = ({ setComplete }: FormProps) => {
  const { flow } = useFlow()
  const flowMessages = useMemo(() => flow?.ui.messages || [], [flow])
  const emailNode = useFlowNode('traits.email')

  const [disabled, setDisabled] = useState<boolean>(true)
  const [email, setEmail] = useState('')
  const { formatMessage } = useIntl()

  const isEmailValid = isValidEmail(email)
  const isFormValid = isEmailValid

  useMemo(() => {
    if (isFormValid) {
      setDisabled(false)
    } else {
      setDisabled(true)
    }
    // eslint-disable-next-line
  }, [email])

  return (
    <Box flexDirection='column' width={['100%', 400, 400]}>
      <Box position='relative'>
        <FlowNode name='email'>
          {(field, value, onChange) => (
            <Column fill>
              <Tooltip
                isOpen={flowMessages.length > 0 || (emailNode && emailNode?.messages.length > 0)}
                container={
                  <TooltipMessages messages={flowMessages.concat(emailNode?.messages ?? [])} />
                }
              >
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
                      id: 'recovery_password.email',
                      defaultMessage: 'Электронная почта',
                    })}
                  />
                </Layout>
              </Tooltip>
            </Column>
          )}
        </FlowNode>
        <Box position='absolute' top={15} right={16}>
          <Condition match={isEmailValid}>
            <DoneAllIcon width={18} height={10} />
          </Condition>
        </Box>
      </Box>
      <Layout flexBasis={16} flexShrink={0} />
      <Row>
        <FlowSubmit>
          {({ submitting, onSubmit }) => (
            <Button
              size='withoutPaddingMiddlingHeight'
              variant='redBackground'
              fill
              disabled={submitting || disabled}
              onClick={() => {
                onSubmit({ method: 'link' })
                setComplete()
              }}
            >
              <Text fontWeight='medium' fontSize='medium'>
                <Condition match={submitting || disabled}>
                  <FormattedMessage id='recovery_password.continue' defaultMessage='Продолжить' />
                </Condition>
                <Condition match={!submitting && !disabled}>
                  <FormattedMessage id='recovery_password.login' defaultMessage='Войти' />
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
