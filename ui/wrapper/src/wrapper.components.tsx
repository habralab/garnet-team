import React                 from 'react'
import { CSSProperties }     from 'react'
import { PropsWithChildren } from 'react'
import { FC }                from 'react'

import { Background }        from '@ui/background'
import { BackgroundProps }   from '@ui/background'
import { Box }               from '@ui/layout'
import { Column }            from '@ui/layout'
import { Layout }            from '@ui/layout'

type WrapperProps = PropsWithChildren<Omit<BackgroundProps, 'color'>> & { style?: CSSProperties }

const paddings = 30

export const WrapperMaxWidth: FC<WrapperProps> = ({ children, ...props }) => (
  <Box
    width='100%'
    maxWidth={1296 + paddings}
    padding={`0 ${paddings / 2}px`}
    marginBottom={32}
    {...props}
  >
    {children}
  </Box>
)

export const WrapperWhite: FC<WrapperProps> = ({ children, ...props }) => (
  <Background
    fill
    borderRadius='medium'
    boxShadow='black'
    position='relative'
    color='white'
    {...props}
  >
    <Layout flexBasis={32} />
    <Column fill>
      <Layout flexBasis={32} />
      {children}
      <Layout flexBasis={32} />
    </Column>
    <Layout flexBasis={32} />
  </Background>
)
