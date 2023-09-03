/* eslint-disable jsx-a11y/anchor-is-valid */

import { Column }           from '@atls-ui-proto/layout'
import { Layout }           from '@atls-ui-proto/layout'
import { Box }              from '@atls-ui-proto/layout'
import { LogoPlaceholder }  from '@atls-ui-proto/placeholder'
import { Text }             from '@atls-ui-proto/text'

import Link                 from 'next/link'
import React                from 'react'
import { FormattedMessage } from 'react-intl'
import { useRouter }        from 'next/router'

const SidebarLink = ({ href, icon, children, exact }: any) => {
  const { pathname } = useRouter()

  const match = exact ? pathname === href : pathname.startsWith(href)

  return (
    <Link href={href}>
      <a style={{ textDecoration: 'none', width: '100%' }}>
        <Box
          p='16px 16px 16px 32px'
          position='relative'
          height={48}
          width='100%'
          bg={match ? '#F3F4F8' : 'transparent'}
          borderRadius={12}
          style={{ cursor: 'pointer' }}
        >
          <Box position='absolute' width={64} minHeight='100%' alignItems='center' left={0} top={0}>
            {icon}
          </Box>
          <Text fontSize={14} color={match ? 'black' : '#606570'} fontWeight={match ? 500 : 400}>
            {children}
          </Text>
        </Box>
      </a>
    </Link>
  )
}

export const ProfileSidebar = () => (
  <Column px={[24, 24, 40]}>
    <Layout flexBasis={32} />
    <Layout pl={15}>
      <LogoPlaceholder>
        <FormattedMessage id='profile_sidebar.project_starter' defaultMessage='Project Starter' />
      </LogoPlaceholder>
    </Layout>
    <Layout flexBasis={60} />
    <Layout>
      <SidebarLink href='/profile/settings'>
        <FormattedMessage id='profile_sidebar.profile' defaultMessage='Профиль' />
      </SidebarLink>
    </Layout>
    <Layout flexGrow={1} />
    <Layout>
      <Link href='/auth/logout'>
        <a style={{ textDecoration: 'none' }}>
          <Text fontSize={16} fontWeight={500}>
            <FormattedMessage id='profile_sidebar.logout' defaultMessage='Выйти' />
          </Text>
        </a>
      </Link>
    </Layout>
    <Layout flexBasis={24} />
  </Column>
)
