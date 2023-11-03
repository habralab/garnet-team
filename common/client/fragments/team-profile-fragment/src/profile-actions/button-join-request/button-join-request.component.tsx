import React                        from 'react'
import { FC }                       from 'react'
import { FormattedMessage }         from 'react-intl'
import { useState }                 from 'react'

import { Button }                   from '@ui/button'
import { Condition }                from '@ui/condition'
import { Layout }                   from '@ui/layout'
import { Text }                     from '@ui/text'

import { ButtonJoinRequestProps }   from './button-join-request.interfaces'
import { useCancelTeamJoinRequest } from '../../data'
import { useCreateTeamJoinRequest } from '../../data'

export const ButtonJoinRequest: FC<ButtonJoinRequestProps> = ({ joinRequest, team }) => {
  const [currentJoinRequest, setCurrentJoinRequest] = useState(joinRequest)

  const { createTeamJoinRequest } = useCreateTeamJoinRequest()
  const { cancelTeamJoinRequest } = useCancelTeamJoinRequest()

  const handleRequest = async () => {
    try {
      const { data } = await createTeamJoinRequest({ variables: { id: team?.id } })

      setCurrentJoinRequest(data?.teamUserJoinRequestCreate.teamUserJoinRequestPayload)
    } catch (error) {
      /** @todo error notification */
    }
  }

  const handleCancelRequest = async () => {
    try {
      await cancelTeamJoinRequest({ variables: { id: currentJoinRequest?.id } })

      setCurrentJoinRequest(undefined)
    } catch (error) {
      /** @todo error notification */
    }
  }

  return (
    <>
      <Condition match={Boolean(currentJoinRequest)}>
        <Button variant='link' size='micro' onClick={handleCancelRequest}>
          <Text fontSize='medium' color='currentColor'>
            <FormattedMessage id='profile.cancel_request' />
          </Text>
        </Button>
      </Condition>
      <Layout width={62} />
      <Button
        variant='primary'
        size='normal'
        disabled={Boolean(currentJoinRequest)}
        onClick={handleRequest}
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
