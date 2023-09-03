import { Layout }           from '@atls-ui-proto/layout'
import { Column }           from '@atls-ui-proto/layout'
import { Text }             from '@atls-ui-proto/text'

import React                from 'react'
import { FormattedMessage } from 'react-intl'

import { Box }              from '@ui/layout'
import { Row }              from '@ui/layout'
import { Logo }             from '@ui/logo'
import { useFlow }          from '@fork/next-identity-integration'

export const Complete = () => {
  const { flow } = useFlow()
  // @ts-ignore
  const email =
    // @ts-ignore
    flow?.ui?.nodes?.find(({ attributes }) => attributes.name === 'email').attributes.value

  return (
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
              defaultMessage={`Отправили письмо на ${email}`}
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
              defaultMessage='Пожалуйста, перейдите по ссылке в письме для восстановления пароля'
            />
          </Text>
        </Box>
        <Layout flexBasis={[40, 60, 0]} flexShrink={0} />
      </Column>
      <Layout flexBasis={[12, 24, 30]} flexShrink={0} />
    </Row>
  )
}
