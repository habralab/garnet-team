import React                from 'react'
import { FormattedMessage } from 'react-intl'

import { GoToRegistration } from '@identity/registration-password-fragment'
import { ImageBlock }       from '@ui/image'
import { Box }              from '@ui/layout'
import { Column }           from '@ui/layout'
import { Row }              from '@ui/layout'
import { Layout }           from '@ui/layout'
import { Logo }             from '@ui/logo'
import { Text }             from '@ui/text'

import { Form }             from './form'
import { RecoveryLink }     from './recovery-link.component'

export const LoginPassword = () => (
  <Row height={['auto', '100vh', '100vh']}>
    <Row height='100%' display={['none', 'none', 'flex']}>
      <ImageBlock src='../img/background-registration.png' />
    </Row>
    <Box
      height='100%'
      width={['100%', '100%', 680]}
      flexShrink={0}
      backgroundColor={['background.white', 'background.white', 'background.veryLightGray']}
    >
      <Layout flexBasis={[12, 24, 30]} flexShrink={0} />
      <Column justifyContent={['start', 'center', 'center']} alignItems='center' width='100%'>
        <Layout flexBasis={[40, 60, 0]} flexShrink={0} />
        <Box width={153}>
          <Logo />
        </Box>
        <Layout flexBasis={36} flexShrink={0} />
        <Box justifyContent='center'>
          <Text fontWeight='semiBold' fontSize='semiRegular' lineHeight='medium'>
            <FormattedMessage id='app_login_password.login' defaultMessage='Вход' />
          </Text>
        </Box>
        <Layout flexBasis={24} flexShrink={0} />
        <Form />
        <Layout flexBasis={8} flexShrink={0} />
        <Box justifyContent='space-between' width={['100%', 400, 400]}>
          <GoToRegistration />
          <RecoveryLink />
        </Box>
        <Layout flexBasis={[40, 60, 0]} flexShrink={0} />
      </Column>
      <Layout flexBasis={[12, 24, 30]} flexShrink={0} />
    </Box>
  </Row>
)
