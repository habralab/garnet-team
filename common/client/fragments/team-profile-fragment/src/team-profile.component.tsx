import React                   from 'react'
import { FC }                  from 'react'
import { FormattedMessage }    from 'react-intl'
import { useRouter }           from 'next/router'
import { useEffect }           from 'react'
import { useState }            from 'react'

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
import { mockAuthUserId }      from '@shared/data'
import { useGetUser }          from '@shared/data'

import { ProfileActions }      from './profile-actions'
import { ProfileDescription }  from './profile-description'
import { ProfileParticipants } from './profile-participants'
import { ProfileProjects }     from './profile-projects'
import { useGetTeam }          from './data'

export const TeamProfile: FC = () => {
  const router = useRouter()
  const queryId = typeof router.query.id === 'string' ? router.query.id : ''

  const [team, setTeam] = useState<Team>()
  const [isMyTeam, setIsMyTeam] = useState(false)

  const {
    team: fetchedTeam,
    teamProjects,
    teamParticipants,
    applicationParticipants,
    invitedParticipants,
  } = useGetTeam({ id: queryId, search: '', skip: 0, take: 0 })

  const { user: ownerUser } = useGetUser({ id: team?.ownerUserId || '', skip: 0, take: 20 })

  useEffect(() => {
    if (fetchedTeam) {
      if (fetchedTeam.ownerUserId === mockAuthUserId) setIsMyTeam(true)

      setTeam(fetchedTeam)
    }
  }, [router, fetchedTeam])

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
              <ProfileActions team={team} isMyTeam={isMyTeam} onEditTeam={handleEditTeam} />
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
