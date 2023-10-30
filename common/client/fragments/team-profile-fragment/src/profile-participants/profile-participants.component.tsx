import React                        from 'react'
import { FC }                       from 'react'
import { FormattedMessage }         from 'react-intl'

import { UserRequestBlock }         from '@shared/modals-fragment'
import { Avatar }                   from '@ui/avatar'
import { Background }               from '@ui/background'
import { Button }                   from '@ui/button'
import { Condition }                from '@ui/condition'
import { Box }                      from '@ui/layout'
import { Column }                   from '@ui/layout'
import { Row }                      from '@ui/layout'
import { Layout }                   from '@ui/layout'
import { NextLink }                 from '@ui/link'
import { Text }                     from '@ui/text'

import { ButtonManageParticipants } from './button-manage-participants'
import { ProfileParticipantsProps } from './profile-participants.interfaces'

export const ProfileParticipants: FC<ProfileParticipantsProps> = (props) => {
  const {
    isMyTeam = false,
    applicationParticipants = [],
    invitedParticipants = [],
    participants = [],
  } = props

  return (
    <Column fill>
      <Row justifyContent='space-between' alignItems='center'>
        <Text fontSize='regular' fontWeight='bold' color='text.secondary'>
          <FormattedMessage id='profile.participants' />
          <Condition match={participants.length > 0}>{`: ${participants.length}`}</Condition>
          <Condition match={isMyTeam && applicationParticipants.length > 0}>
            <Layout width={10} />
            <Background
              width={24}
              color='red'
              borderRadius='half'
              alignItems='center'
              justifyContent='center'
            >
              <Text fontSize='normal' color='text.white'>
                +{applicationParticipants.length}
              </Text>
            </Background>
          </Condition>
        </Text>
        <Condition match={isMyTeam && participants.length > 0}>
          <ButtonManageParticipants {...props}>
            <FormattedMessage id='profile.manage' />
          </ButtonManageParticipants>
        </Condition>
      </Row>
      <Layout flexBasis={30} flexShrink={0} />
      <Condition match={participants.length > 0}>
        <Row
          display='grid'
          style={{ gap: 17, gridTemplateColumns: `repeat(auto-fill, 74px)` }}
          justifyContent='space-between'
        >
          {participants.slice(0, 12).map(({ id, avatarUrl, userName }) => (
            <Avatar key={id} image={avatarUrl} size={74} title={userName} url={`/user/${id}`} />
          ))}
        </Row>
        <Condition match={participants.length > 12}>
          <Layout flexBasis={20} flexShrink={0} />
          <Box justifyContent='flex-end'>
            <ButtonManageParticipants {...props}>
              <FormattedMessage id='profile.more_plus' values={{ number: participants.length }} />
            </ButtonManageParticipants>
          </Box>
        </Condition>
      </Condition>
      <Condition match={participants.length === 0}>
        <Column justifyContent='center' alignItems='center' height={90}>
          <Text
            fontSize='semiLarge'
            fontWeight='semiBold'
            color='text.gray'
            textAlign='center'
            opacity={0.6}
          >
            <FormattedMessage id='profile.no_participants' />
          </Text>
          <Condition match={isMyTeam}>
            <Layout flexBasis={10} flexShrink={0} />
            <NextLink path='/team/invite' href='/team/invite'>
              <Button variant='link' size='micro'>
                <Text fontSize='normal' color='currentColor'>
                  <FormattedMessage id='profile.find_participants' />
                </Text>
              </Button>
            </NextLink>
          </Condition>
        </Column>
      </Condition>
      <Condition match={isMyTeam && applicationParticipants.length > 0}>
        <Layout flexBasis={30} flexShrink={0} />
        <Text fontSize='medium' color='text.secondary'>
          <FormattedMessage id='profile.applications' />
        </Text>
        <Layout flexBasis={12} flexShrink={0} />
        {applicationParticipants.slice(0, 1).map((user) => (
          <UserRequestBlock user={user} />
        ))}
        <Condition match={applicationParticipants.length > 1}>
          <Layout flexBasis={20} flexShrink={0} />
          <Box justifyContent='flex-end'>
            <ButtonManageParticipants {...props}>
              <FormattedMessage
                id='profile.more_plus'
                values={{ number: applicationParticipants.length }}
              />
            </ButtonManageParticipants>
          </Box>
        </Condition>
      </Condition>
      <Condition match={isMyTeam && invitedParticipants.length > 0}>
        <Layout flexBasis={30} flexShrink={0} />
        <Text fontSize='medium' color='text.secondary'>
          <FormattedMessage id='profile.invitations' />
        </Text>
        <Layout flexBasis={16} flexShrink={0} />
        {invitedParticipants.slice(0, 1).map((user) => (
          <UserRequestBlock user={user} />
        ))}
        <Condition match={invitedParticipants.length > 1}>
          <Layout flexBasis={20} flexShrink={0} />
          <Box justifyContent='flex-end'>
            <ButtonManageParticipants {...props}>
              <FormattedMessage
                id='profile.more_plus'
                values={{ number: invitedParticipants.length }}
              />
            </ButtonManageParticipants>
          </Box>
        </Condition>
      </Condition>
    </Column>
  )
}
