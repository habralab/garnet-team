import React                from 'react'
import { FormattedMessage } from 'react-intl'

import { Layout }           from '@ui/layout'
import { Column }           from '@ui/layout'
import { Box }              from '@ui/layout'
import { Row }              from '@ui/layout'
import { Logo }             from '@ui/logo'
import { Text }             from '@ui/text'
import { useFlow }          from '@fork/next-identity-integration'

export const Complete = () => {
  const { loading, identity } = useFlow()

  const email = identity?.traits?.email
  const firstName = identity?.traits?.name.first

  return !loading ? (
    <Row height='100vh'>
      <Layout flexBasis={[12, 24, 30]} flexShrink={0} />
      <Column width='100%' justifyContent={['start', 'center', 'center']} alignItems='center'>
        <Layout flexBasis={[40, 60, 0]} flexShrink={0} />
        <Box width={[215, 315, 400]}>
          <Logo />
        </Box>
        <Layout flexBasis={[32, 48, 64]} flexShrink={0} />
        <Box justifyContent='center'>
          <Text
            fontWeight='semiBold'
            fontSize='semiRegular'
            lineHeight='medium'
            color='text.black'
            textAlign='center'
          >
            <FormattedMessage
              id='app_letter_sent.anna_we_sent_you_anemail'
              defaultMessage={`${firstName}, мы отправили вам письмо на ${email}`}
            />
          </Text>
        </Box>
        <Layout flexBasis={16} flexShrink={0} />
        <Box justifyContent='center' width={['100%', 471, 471]}>
          <Text
            fontWeight='semiBold'
            fontSize='semiMedium'
            lineHeight='primary'
            color='text.black'
            textAlign='center'
          >
            <FormattedMessage
              id='app_letter_sent.please_follow_the_link_in_the_email'
              defaultMessage='Пожалуйста, перейдите по ссылке в письме для подтверждения регистрации'
            />
          </Text>
        </Box>
        <Layout flexBasis={[40, 60, 0]} flexShrink={0} />
      </Column>
      <Layout flexBasis={[12, 24, 30]} flexShrink={0} />
    </Row>
  ) : null
}
