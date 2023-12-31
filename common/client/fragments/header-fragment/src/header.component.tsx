import React                from 'react'
import { Fragment }         from 'react'
import { FC }               from 'react'
import { FormattedMessage } from 'react-intl'
import { useRouter }        from 'next/router'

import { Background }       from '@ui/background'
import { Condition }        from '@ui/condition'
import { Layout }           from '@ui/layout'
import { Row }              from '@ui/layout'
import { Column }           from '@ui/layout'
import { Box }              from '@ui/layout'
import { NextLink }         from '@ui/link'
import { Logo }             from '@ui/logo'

import { ButtonExit }       from './button-exit'
import { HeaderProps }      from './header.interfaces'
import { Notifications }    from './notifications'
import { TabItem }          from './tab-item'
import { navigationItems }  from './header.constants'
import { isActiveLink }     from './helpers'

export const Header: FC<HeaderProps> = ({ disableNavigation = false }) => {
  const { pathname } = useRouter()

  return (
    <Background fill maxHeight={120} color='white' borderBottom='lightGrayForty'>
      <Column fill>
        <Layout flexBasis={36.5} flexShrink={0} />
        <Row justifyContent='space-between' alignItems='center'>
          <Box height={47}>
            <Layout flexBasis={24} flexShrink={0} />
            <Box minWidth={202}>
              <Logo withLink={!disableNavigation} />
            </Box>
          </Box>
          <Condition match={!disableNavigation}>
            <Layout flexBasis={100} flexShrink={0} />
            {navigationItems.map(({ id, url }) => (
              <Fragment key={id}>
                <NextLink path={url} active>
                  <TabItem active={isActiveLink(url, pathname)}>
                    <FormattedMessage id={id} />
                  </TabItem>
                </NextLink>
                <Layout flexBasis={30} flexShrink={0} />
              </Fragment>
            ))}
            <Box fill alignItems='center' justifyContent='flex-end'>
              <Notifications />
              <Layout flexBasis={20} flexShrink={0} />
              <ButtonExit />
              <Layout flexBasis={24} flexShrink={0} />
            </Box>
          </Condition>
        </Row>
        <Layout flexBasis={36.5} flexShrink={0} />
      </Column>
    </Background>
  )
}
