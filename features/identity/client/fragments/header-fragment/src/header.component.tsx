import React          from 'react'
import { FC }         from 'react'

import { Background } from '@ui/background'
import { Layout }     from '@ui/layout'
import { Row }        from '@ui/layout'
import { Column }     from '@ui/layout'
import { Box }        from '@ui/layout'
import { Logo }       from '@ui/logo'

import { LinkSwitch } from './link-switch'

export const Header: FC = () => (
  <Background fill maxHeight={120} color='white' borderBottom='lightGrayForty'>
    <Column fill>
      <Layout flexBasis={36.5} flexShrink={0} />
      <Row justifyContent='space-between' alignItems='center'>
        <Layout flexBasis={24} flexShrink={0} />
        <Box minWidth={202} height={47}>
          <Logo />
        </Box>
        <Layout flexBasis={100} flexShrink={0} />
        <LinkSwitch />
      </Row>
      <Layout flexBasis={36.5} flexShrink={0} />
    </Column>
  </Background>
)
