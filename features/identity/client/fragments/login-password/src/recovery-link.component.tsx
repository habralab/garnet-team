import React                from 'react'
import { FormattedMessage } from 'react-intl'
import { useRouter }        from 'next/router'

import { Box }              from '@ui/layout'
import { Link }             from '@ui/link'
import { Text }             from '@ui/text'

export const RecoveryLink = () => {
  const { query } = useRouter()

  return (
    <Box>
      <Link
        href={query.return_to ? `/auth/recovery?return_to=${query.return_to}` : '/auth/recovery'}
      >
        <Text
          fontWeight='semiMedium'
          fontSize='micro'
          lineHeight='medium'
          color='text.black'
          style={{ textDecoration: 'underline' }}
        >
          <FormattedMessage id='app_login_password.forgot' defaultMessage='Забыли пароль?' />
        </Text>
      </Link>
    </Box>
  )
}
