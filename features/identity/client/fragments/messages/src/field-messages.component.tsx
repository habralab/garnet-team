import React                  from 'react'

import { Column }             from '@ui/layout'
import { Layout }             from '@ui/layout'
import { Text }               from '@ui/text'

import { FieldMessagesProps } from './messages.interfaces'

export const FieldMessages = ({ messages = [] }: FieldMessagesProps) => (
  <Column>
    {messages.map((message) => (
      <Layout key={message?.text}>
        <Text
          key={message?.id}
          color={message?.type === 'error' ? '#E41508' : 'black'}
          lineHeight='24px'
        >
          {message?.text}
        </Text>
      </Layout>
    ))}
  </Column>
)
