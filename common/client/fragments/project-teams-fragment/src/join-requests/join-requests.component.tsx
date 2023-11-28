import React                               from 'react'
import { FC }                              from 'react'
import { FormattedMessage }                from 'react-intl'

import { Avatar }                          from '@ui/avatar'
import { Button }                          from '@ui/button'
import { Column }                          from '@ui/layout'
import { Row }                             from '@ui/layout'
import { Layout }                          from '@ui/layout'
import { Space }                           from '@ui/text'
import { Text }                            from '@ui/text'
import { routes }                          from '@shared/routes'

import { JoinRequestsProps }               from './join-requests.interfaces'
import { useDecideProjectTeamJoinRequest } from '../data'

export const JoinRequests: FC<JoinRequestsProps> = ({ joinRequests }) => {
  const { decideProjectTeamJoinRequest, loading } = useDecideProjectTeamJoinRequest()

  const handleDecideProjectTeamJoinRequest = (approve: boolean, requestId?: string) => async () => {
    try {
      await decideProjectTeamJoinRequest({ variables: { approve, id: requestId } })
    } catch (error) {
      if (process.env.NODE_ENV !== 'production') throw error
    }
  }

  return (
    <Column fill>
      <Text fontSize='regular' fontWeight='bold' color='text.secondary'>
        <FormattedMessage id='project_teams.new_applications' />: {joinRequests.length}
      </Text>
      <Layout flexBasis={42} flexShrink={0} />
      <Column fill id='custom-scrollbar' maxHeight={510} overflow='auto' gap={30}>
        {joinRequests.map((item) => (
          <Row fill maxHeight={150}>
            <Avatar
              size={150}
              shape='square'
              image={item?.teamAvatarUrl}
              title={item.teamName}
              url={`${routes.teams}/${item.teamId}`}
            />
            <Layout flexBasis={20} flexShrink={0} />
            <Column flex='1 1 500px' justifyContent='space-between'>
              <Text fontSize='preLarge' fontWeight='bold' color='text.secondary'>
                {item.teamName}
              </Text>
              <Layout flexBasis={20} flexShrink={0} />
              <Row>
                <Text fontSize='semiMedium' color='text.gray'>
                  <FormattedMessage id='project_teams.participants' />:
                  <Space />
                  {item.teamParticipants?.length}
                </Text>
                <Layout flexBasis={32} flexShrink={0} />
                <Text fontSize='semiMedium' color='text.gray'>
                  <FormattedMessage id='project_teams.projects' />: {item.teamProjectCount}
                </Text>
              </Row>
              <Layout flexBasis={6} flexShrink={0} />
              <Text fontSize='medium' color='text.secondary' overflow='hidden' maxHeight='3.6em'>
                {item.teamDescription}
              </Text>
            </Column>
            <Layout flexBasis={20} flexShrink={0} />
            <Row fill flex='1 1 320px' alignItems='center' justifyContent='flex-end'>
              <Button
                variant='link'
                size='micro'
                disabled={loading}
                onClick={handleDecideProjectTeamJoinRequest(false, item.id)}
              >
                <Text fontSize='normal' color='currentColor'>
                  <FormattedMessage id='project_teams.reject' />
                </Text>
              </Button>
              <Layout flexBasis={30} />
              <Button
                variant='secondary'
                size='small'
                disabled={loading}
                onClick={handleDecideProjectTeamJoinRequest(true, item.id)}
              >
                <Text fontSize='normal' color='currentColor'>
                  <FormattedMessage id='project_teams.accept_application' />
                </Text>
              </Button>
            </Row>
            <Layout flexBasis={4} flexShrink={0} />
          </Row>
        ))}
      </Column>
    </Column>
  )
}
