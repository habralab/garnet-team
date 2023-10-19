import { FlowSubmit }       from '@atls/next-identity-integration'
import { FlowNode }         from '@atls/next-identity-integration'
import { Background }       from '@ui/background/src'
import { Button }           from '@ui/button/src'
import { EyeIcon }          from '@ui/icon/src'
import { useState }         from 'react'
import React                from 'react'
import { useIntl }          from 'react-intl'
import { FormattedMessage } from 'react-intl'

import { Input }       from '@ui/input'
import { Box, Column } from '@ui/layout'
import { Layout }      from '@ui/layout'
import { Text }        from '@ui/text'

export const RegistrationForm = () => {
  const [showPassword, setShowPassword] = useState<boolean>(false)
  const [showPasswordRepeat, setShowPasswordRepeat] = useState<boolean>(false)

  const { formatMessage } = useIntl()

  const togglePassword = () => setShowPassword(!showPassword)
  const togglePasswordRepeat = () => setShowPasswordRepeat(!showPasswordRepeat)

  return (
    <Background color='white' borderRadius='medium' boxShadow='black'>
      <Column paddingX={32} width={342} boxSizing='content-box'>
        <Layout flexBasis={56} flexShrink={0}/>
        <Layout justifyContent='center'>
          <Text fontSize='preLarge' width='min-content' textAlign='center'>
            <FormattedMessage id={'registration_password.title'}/>
          </Text>
        </Layout>
        <Layout flexBasis={40} flexShrink={0}/>
        <Input placeholder={formatMessage({ id: 'registration_password.name' })}/>
        <Layout flexBasis={16} flexShrink={0}/>
        <Input placeholder={formatMessage({ id: 'registration_password.last_name' })}/>
        <Layout flexBasis={40} flexShrink={0}/>
        <FlowNode name='traits.email'>
          {(field, value, onChange) => (
            <Input
              placeholder={formatMessage({ id: 'registration_password.email' })}
              {...field.attributes}
              value={value}
              onChange={onChange}/>
          )}
        </FlowNode>
        <Layout flexBasis={40} flexShrink={0}/>
        <Box position='relative'>
          <FlowNode name='password'>
            {(field, value, onChange) => (
              <Input
                placeholder={formatMessage({ id: 'registration_password.password' })}
                value={value}
                onChange={onChange}
                iconSvg={<EyeIcon/>}
                valueWidth={16}
                valueHeight={16}
                onIconClick={togglePassword}
                {...field.attributes}/>
            )}
          </FlowNode>
        </Box>
        <Layout flexBasis={40} flexShrink={0}/>
        <Input
          placeholder={formatMessage({ id: 'registration_password.repeat_password' })}
          onIconClick={togglePasswordRepeat}
          iconSvg={<EyeIcon/>}
          valueWidth={16}
          valueHeight={16}
          type={showPasswordRepeat ? 'text' : 'password'}
        />
        <Layout flexBasis={40} flexShrink={0}/>
        <FlowSubmit>
          {({ submitting, onSubmit }) => (
            <Button disabled={submitting} onClick={() =>
              onSubmit({
                // traits: {
                //   email,
                //   name: { first: name.split(' ')[0], last: name.split(' ')[1] },
                // },
                method: 'password',
              })}>
              <FormattedMessage id={'registration_password.register'}/>
            </Button>
          )}
        </FlowSubmit>
        <Layout flexBasis={56} flexShrink={0}/>
      </Column>
    </Background>
  )
}
