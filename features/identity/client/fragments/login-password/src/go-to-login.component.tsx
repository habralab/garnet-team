import React                from 'react'
import { FormattedMessage } from 'react-intl'
import { useRouter }        from 'next/router'

import { Box }              from '@ui/layout'
import { Link }             from '@ui/link'
import { Text }             from '@ui/text'
import { Space }            from '@ui/text'

export const GoToLogin = () => {
  const { query } = useRouter()

  return (
    <Box alignItems='end'>
      <Box>
        <Text fontWeight='semiMedium' fontSize='micro' lineHeight='medium' color='text.black'>
          <FormattedMessage id='app_login_password.have_account' defaultMessage='Есть аккаунт?' />
          <Space />
        </Text>
      </Box>
      <Box>
        <Link href={query.return_to ? `/auth/login?return_to=${query.return_to}` : '/auth/login'}>
          <Text
            fontWeight='semiMedium'
            fontSize='micro'
            lineHeight='medium'
            color='text.black'
            style={{ textDecoration: 'underline' }}
          >
            <FormattedMessage id='app_login_password.login' defaultMessage='Войти' />
          </Text>
        </Link>
      </Box>
    </Box>
  )
}
