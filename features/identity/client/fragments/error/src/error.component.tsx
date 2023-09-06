import React                from 'react'
import { FormattedMessage } from 'react-intl'

import { ErrorNode }        from '@fork/next-identity-integration'
import { Button }           from '@ui/button'
import { Column }           from '@ui/layout'
import { Layout }           from '@ui/layout'
import { Text }             from '@ui/text'

export const Error = () => (
  <Column justifyContent='center' alignItems='center'>
    <Layout maxWidth={448} width='100%'>
      <ErrorNode>
        {({ error }) => (
          <Column alignItems='center'>
            <Layout>
              <Text fontSize={48} color='#A0A4B2' textAlign='center'>
                {error?.status}
              </Text>
            </Layout>
            <Layout flexBasis={20} />
            <Layout>
              <Text fontSize={14} lineHeight='24px' textAlign='center'>
                {error?.message}
              </Text>
            </Layout>
            <Layout flexBasis='8px' />
            <Layout>
              <Text fontSize={14} lineHeight='24px' textAlign='center'>
                {error?.reason}
              </Text>
            </Layout>
          </Column>
        )}
      </ErrorNode>
    </Layout>
    <Layout flexBasis={70} />
    <Layout>
      {/* @ts-ignore */}
      <Button as='a' href='/auth/login' size='large' style={{ textDecoration: 'none' }}>
        <FormattedMessage id='error.to_login_page' defaultMessage='На страницу авторизации' />
      </Button>
    </Layout>
  </Column>
)
