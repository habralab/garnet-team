import React                      from 'react'
import { FC }                     from 'react'
import { FormattedMessage }       from 'react-intl'
import { useState }               from 'react'

import { Button }                 from '@ui/button'
import { Condition }              from '@ui/condition'
import { Layout }                 from '@ui/layout'
import { Text }                   from '@ui/text'

import { ButtonJoinRequestProps } from './button-join-request.interfaces'
import { useCreateRequest }       from '../../hooks'
import { useCancelRequest }       from '../../hooks'

export const ButtonJoinRequest: FC<ButtonJoinRequestProps> = ({ joinRequest, team }) => {
  const [currentJoinRequest, setCurrentJoinRequest] = useState(joinRequest)

  const { createRequest, loading: loadingCreate } = useCreateRequest()
  const { cancelRequest, loading: loadingCancel } = useCancelRequest()

  const handleCreateRequest = async () => {
    const newJoinRequest = await createRequest(team?.id)
    setCurrentJoinRequest(newJoinRequest)
  }

  const handleCancelRequest = async () => {
    await cancelRequest(currentJoinRequest?.id)
    setCurrentJoinRequest(undefined)
  }

  return (
    <>
      <Condition match={Boolean(currentJoinRequest)}>
        <Button variant='link' size='micro' disabled={loadingCancel} onClick={handleCancelRequest}>
          <Text fontSize='medium' color='currentColor'>
            <FormattedMessage id='profile.cancel_request' />
          </Text>
        </Button>
      </Condition>
      <Layout width={62} />
      <Button
        variant='primary'
        size='normal'
        disabled={Boolean(currentJoinRequest) || loadingCreate}
        onClick={handleCreateRequest}
      >
        <Text fontSize='medium' color='currentColor'>
          <Condition match={Boolean(currentJoinRequest)}>
            <FormattedMessage id='profile.request_sent' />
          </Condition>
          <Condition match={!currentJoinRequest}>
            <FormattedMessage id='profile.send_request' />
          </Condition>
        </Text>
      </Button>
    </>
  )
}
