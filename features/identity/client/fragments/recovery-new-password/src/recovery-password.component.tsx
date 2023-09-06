import React                from 'react'
import { FormattedMessage } from 'react-intl'

import { Column }           from '@ui/layout'
import { Layout }           from '@ui/layout'
import { Row }              from '@ui/layout'
import { Box }              from '@ui/layout'
import { Logo }             from '@ui/logo'
import { Text }             from '@ui/text'

import { Form }             from './form'

export const RecoveryNewPassword = () => (
  <Row height={['auto', '100vh', '100vh']}>
    <Box
      height='100%'
      width='100%'
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
            <FormattedMessage
              id='app_registration.password-recovery'
              defaultMessage='Восстановление пароля'
            />
          </Text>
        </Box>
        <Layout flexBasis={24} flexShrink={0} />
        <Form />
        <Layout flexBasis={8} flexShrink={0} />
      </Column>
      <Layout flexBasis={[12, 24, 30]} flexShrink={0} />
    </Box>
  </Row>
)
