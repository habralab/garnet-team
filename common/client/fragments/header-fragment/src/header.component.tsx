import React                from 'react'
import { Fragment }         from 'react'
import { FC }               from 'react'
import { FormattedMessage } from 'react-intl'
import { useRouter }        from 'next/router'

import { Background }       from '@ui/background'
import { Layout }           from '@ui/layout'
import { Row }              from '@ui/layout'
import { Column }           from '@ui/layout'
import { Box }              from '@ui/layout'
import { NextLink }         from '@ui/link'
import { Logo }             from '@ui/logo'

import { ButtonExit }       from './button-exit'
import { TabItem }          from './tab-item'
import { navigationItems }  from './header.constants'

export const Header: FC = () => {
  const { pathname } = useRouter()

  const currentPathname = `/${pathname.split('/')[1]}`

  return (
    <Background fill maxHeight={120} color='white' borderBottom='lightGrayForty'>
      <Column fill>
        <Layout flexBasis={36.5} flexShrink={0} />
        <Row justifyContent='space-between' alignItems='center'>
          <Layout flexBasis={24} flexShrink={0} />
          <Box minWidth={202} height={47}>
            <Logo />
          </Box>
          <Layout flexBasis={100} flexShrink={0} />
          {navigationItems.map(({ id, url }) => (
            <Fragment key={id}>
              <NextLink path={url} active>
                <TabItem active={url === currentPathname}>
                  <FormattedMessage id={id} />
                </TabItem>
              </NextLink>
              <Layout flexBasis={30} flexShrink={0} />
            </Fragment>
          ))}
          <ButtonExit />
        </Row>
        <Layout flexBasis={36.5} flexShrink={0} />
      </Column>
    </Background>
  )
}
