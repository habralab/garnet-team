import React                    from 'react'

import { Layout }               from '@ui/layout'
import { Column }               from '@ui/layout'
import { Text }                 from '@ui/text'

import { GeneralMessagesProps } from './messages.interfaces'

export const GeneralMessages = ({ messages = [] }: GeneralMessagesProps) => (
  <Column justifyContent='center' alignItems='center'>
    {messages.map((message) => (
      <Layout maxWidth={320} width='100%' justifyContent='center' key={message?.id}>
        <Text
          color={message?.type === 'error' ? '#E41508' : 'black'}
          fontSize={14}
          lineHeight='24px'
          textAlign='center'
        >
          {message?.text}
        </Text>
      </Layout>
    ))}
    <Layout flexBasis={24} />
  </Column>
)
