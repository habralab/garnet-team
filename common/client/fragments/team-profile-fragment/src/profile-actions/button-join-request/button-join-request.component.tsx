import React                      from 'react'
import { FC }                     from 'react'
import { FormattedMessage }       from 'react-intl'
import { useState }               from 'react'

import { Button }                 from '@ui/button'
import { Condition }              from '@ui/condition'
import { Layout }                 from '@ui/layout'
import { Text }                   from '@ui/text'

import { ButtonJoinRequestProps } from './button-join-request.interfaces'

export const ButtonJoinRequest: FC<ButtonJoinRequestProps> = ({ hasJoinRequest = false }) => {
  const [isRequested, setIsRequested] = useState<boolean>(hasJoinRequest)

  const handleRequest = () => setIsRequested(true)

  const handleCancelRequest = () => setIsRequested(false)

  return (
    <>
      <Condition match={isRequested}>
        <Button variant='link' size='micro' onClick={handleCancelRequest}>
          <Text fontSize='medium' color='currentColor'>
            <FormattedMessage id='profile.cancel_request' />
          </Text>
        </Button>
      </Condition>
      <Layout width={62} />
      <Button variant='primary' size='normal' disabled={isRequested} onClick={handleRequest}>
        <Text fontSize='medium' color='currentColor'>
          <Condition match={isRequested}>
            <FormattedMessage id='profile.request_sent' />
          </Condition>
          <Condition match={!isRequested}>
            <FormattedMessage id='profile.send_request' />
          </Condition>
        </Text>
      </Button>
    </>
  )
}
