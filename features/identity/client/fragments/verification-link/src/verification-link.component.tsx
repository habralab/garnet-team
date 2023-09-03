import React                from 'react'
import { FormattedMessage } from 'react-intl'
import { useRouter }        from 'next/router'

import { FlowSubmit }       from '@fork/next-identity-integration'
import { Button }           from '@ui/button'
import { Column }           from '@ui/layout'
import { Layout }           from '@ui/layout'
import { Box }              from '@ui/layout'
import { Row }              from '@ui/layout'
import { Logo }             from '@ui/logo'
import { Text }             from '@ui/text'

export const VerificationLink = () => {
  const router = useRouter()

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
              defaultMessage='Успешно'
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
              defaultMessage='Ваш аккаунт подтвержден'
            />
          </Text>
          <Layout flexBasis={24} flexShrink={0} />
        </Box>
        <Box flexDirection='column' width={['100%', 400, 400]}>
          <FlowSubmit>
            {({ submitting, onSubmit }) => (
              <Button
                size='withoutPaddingMiddlingHeight'
                variant='redBackground'
                fill
                disabled={submitting}
                onClick={() => {
                  onSubmit()
                  router.push('/auth/login')
                }}
              >
                <Text fontWeight='medium' fontSize='medium'>
                  <FormattedMessage id='app_login_password.continue' defaultMessage='Продолжить' />
                </Text>
              </Button>
            )}
          </FlowSubmit>
        </Box>
        <Layout flexBasis={[40, 60, 0]} flexShrink={0} />
      </Column>
      <Layout flexBasis={[12, 24, 30]} flexShrink={0} />
    </Row>
  )
}
