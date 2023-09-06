/* eslint-disable jsx-a11y/anchor-is-valid */

import { Text }             from '@atls-ui-proto/text'
import { Space }            from '@atls-ui-proto/text'

import Link                 from 'next/link'
import React                from 'react'
import { FormattedMessage } from 'react-intl'
import { useRouter }        from 'next/router'

export const GoToLoginRecovery = () => {
  const { query } = useRouter()

  return (
    <div>
      <Text fontSize={12} color='#A0A4B2' fontWeight={500}>
        <FormattedMessage id='recovery_link.forgot_password' defaultMessage='Вспомнили пароль?' />
      </Text>
      <Space />
      <Link href={query.return_to ? `/auth/login?return_to=${query.return_to}` : '/auth/login'}>
        <a style={{ textDecoration: 'none' }}>
          <Text fontSize={12} fontWeight={500}>
            <FormattedMessage id='recovery_link.enter' defaultMessage='Войти' />
          </Text>
        </a>
      </Link>
    </div>
  )
}
