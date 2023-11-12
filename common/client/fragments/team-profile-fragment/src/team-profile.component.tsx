import React                   from 'react'
import { FC }                  from 'react'
import { FormattedMessage }    from 'react-intl'

import { Team }                from '@shared/data'
import { Avatar }              from '@ui/avatar'
import { Condition }           from '@ui/condition'
import { Box }                 from '@ui/layout'
import { Column }              from '@ui/layout'
import { Row }                 from '@ui/layout'
import { Layout }              from '@ui/layout'
import { Text }                from '@ui/text'
import { Title }               from '@ui/title'
import { WrapperWhite }        from '@ui/wrapper'

import { ProfileActions }      from './profile-actions'
import { ProfileDescription }  from './profile-description'
import { ProfileParticipants } from './profile-participants'
import { ProfileProjects }     from './profile-projects'
import { useTeamState }        from './hooks'

export const TeamProfile: FC = () => {
  const {
    team,
    setTeam,
    isMyTeam,
    ownerUser,
    teamProjects,
    teamParticipants,
    applicationParticipants,
    invitedParticipants,
    joinRequestAuthUser,
  } = useTeamState()

  const handleEditTeam = (editedTeam: Team) => setTeam(editedTeam)

  return (
    <Column fill>
      <Title hasBack>
        <Condition match={isMyTeam && Boolean(team)}>
          <FormattedMessage id='profile.my_team' />
        </Condition>
        <Condition match={!isMyTeam || !team}>
          <FormattedMessage id='profile.team' />
        </Condition>
      </Title>
      <WrapperWhite>
        <Row>
          <Condition match={Boolean(team)}>
            <Avatar size={180} shape='square' image={team?.avatarUrl} />
            <Layout flexBasis={32} />
            <ProfileDescription team={team} />
            <Box position='absolute' bottom={32} right={32} alignItems='center'>
              <ProfileActions
                team={team}
                isMyTeam={isMyTeam}
                onEditTeam={handleEditTeam}
                joinRequest={joinRequestAuthUser}
              />
            </Box>
          </Condition>
          <Condition match={!team}>
            <Column fill alignItems='center'>
              <Layout flexBasis={50} />
              <Text fontSize='semiLarge' fontWeight='semiBold' color='text.gray' textAlign='center'>
                <FormattedMessage id='profile.team_not_found' />
              </Text>
              <Layout flexBasis={50} />
            </Column>
          </Condition>
        </Row>
      </WrapperWhite>
      <Condition match={Boolean(team)}>
        <Layout flexBasis={32} flexShrink={0} />
        <Row justifyContent='space-between'>
          <WrapperWhite maxWidth={850} style={{ height: 'max-content' }}>
            <Column>
              <ProfileProjects ownerUser={ownerUser} projects={teamProjects} isMyTeam={isMyTeam} />
            </Column>
          </WrapperWhite>
          <Layout flexBasis={32} flexShrink={0} />
          <WrapperWhite maxWidth={410} style={{ height: 'max-content' }}>
            <Column>
              <ProfileParticipants
                team={team}
                isMyTeam={isMyTeam}
                ownerUser={ownerUser}
                participants={teamParticipants}
                applicationParticipants={applicationParticipants}
                invitedParticipants={invitedParticipants}
              />
            </Column>
          </WrapperWhite>
        </Row>
      </Condition>
    </Column>
  )
}
