import React                    from 'react'

import { Layout }               from '@ui/layout'
import { Text }                 from '@ui/text'

import ruMessages               from '../ru-messages.json'
import ruReasons                from '../ru-reasons.json'
import { TooltipMessagesProps } from './messages.interfaces'

export const TooltipMessages = ({ messages = [] }: TooltipMessagesProps) => (
  <div>
    {messages.map((message) =>
      message ? (
        <Layout key={message.id}>
          <Text color='white' fontSize={15} lineHeight='18.15px' textAlign='center'>
            {ruMessages
              .find((mes) => mes.id === message.id)
              ?.ru?.replace(
                '{reason}',
                // @ts-ignore
                ruReasons.find((mes) => mes.en === message.context?.reason)?.ru || ''
              ) ?? message.text}
          </Text>
        </Layout>
      ) : null)}
  </div>
)
