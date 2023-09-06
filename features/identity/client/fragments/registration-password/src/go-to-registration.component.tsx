import React                from 'react'
import { FormattedMessage } from 'react-intl'
import { useRouter }        from 'next/router'

import { Box }              from '@ui/layout'
import { Link }             from '@ui/link'
import { Text }             from '@ui/text'
import { Space }            from '@ui/text'

export const GoToRegistration = () => {
  const { query } = useRouter()

  return (
    <Box alignItems='end'>
      <Box>
        <Text fontWeight='semiMedium' fontSize='micro' lineHeight='medium' color='text.black'>
          <FormattedMessage
            id='app_registration_password.no_account'
            defaultMessage='Нет аккаунта?'
          />
          <Space />
        </Text>
      </Box>
      <Box>
        <Link
          href={
            query.return_to
              ? `/auth/registration?return_to=${query.return_to}`
              : '/auth/registration'
          }
        >
          <Text
            fontWeight='semiMedium'
            fontSize='micro'
            lineHeight='medium'
            color='text.black'
            style={{ textDecoration: 'underline' }}
          >
            <FormattedMessage
              id='app_registration_password.register'
              defaultMessage='Зарегистрироваться'
            />
          </Text>
        </Link>
      </Box>
    </Box>
  )
}
