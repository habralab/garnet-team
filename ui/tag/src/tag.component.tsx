import React                 from 'react'
import { PropsWithChildren } from 'react'
import { FC }                from 'react'

import { Background }        from '@ui/background'
import { Text }              from '@ui/text'

export const Tag: FC<PropsWithChildren> = ({ children }) => (
  <Background
    height={31}
    color='lightBlue'
    alignItems='center'
    padding='6px 10px'
    border='blue'
    borderRadius='medium'
  >
    <Text fontSize='normal' color='text.darkBlue'>
      {children}
    </Text>
  </Background>
)
