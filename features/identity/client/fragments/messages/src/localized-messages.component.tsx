import { UiText } from '@ory/kratos-client'
import React      from 'react'

import { Layout } from '@ui/layout'
import { Text }   from '@ui/text'

import ruMessages                 from '../ru-messages.json'
import ruReasons                  from '../ru-reasons.json'
import ruFields                   from '../ru-fields.json'
import { LocalizedMessagesProps } from './messages.interfaces'
import { Condition }              from '@ui/condition'

export const LocalizedMessages = ({ messages = [] }: LocalizedMessagesProps) => (
  <Condition match={messages?.length > 0}>
    {messages.map((message: UiText) =>
      <Condition match={Boolean(message)}>
        <Layout key={message.id}>
          <Text
            color={message.type === 'error' ? 'text.error' : 'text.primary'}
            fontSize='normal'
            textAlign='start'
          >
            {ruMessages.find((mes) => mes.id === message.id)?.ru
              ?.replace(
                '{reason}',
                // @ts-ignore
                ruReasons.find((mes) => message.context?.reason?.includes(mes.en) || message.text.includes(mes.en))?.ru
                // @ts-ignore
                ?? message.context?.reason ?? message.text ?? ''
              )
              .replace(
                '{field}',
                // @ts-ignore
                ruFields.find((mes) => mes.en === message.context?.property)?.ru
                // @ts-ignore
                ?? message.context?.property ?? '') ?? message.text}
          </Text>
        </Layout>
      </Condition>
    )}
  </Condition>
)
