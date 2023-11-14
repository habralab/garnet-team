import React                 from 'react'
import { FC }                from 'react'
import { FormattedMessage }  from 'react-intl'

import { Button }            from '@ui/button'
import { CardTeam }          from '@ui/card'
import { Condition }         from '@ui/condition'
import { Box }               from '@ui/layout'
import { Column }            from '@ui/layout'
import { Row }               from '@ui/layout'
import { Grid }              from '@ui/layout'
import { Layout }            from '@ui/layout'
import { NextLink }          from '@ui/link'
import { Text }              from '@ui/text'
import { routes }            from '@shared/routes'

import { ButtonCreateTeam }  from './button-create-team'
import { ProfileTeamsProps } from './profile-teams.interfaces'

export const ProfileTeams: FC<ProfileTeamsProps> = ({ teams, isMyProject, ownerUser }) => (
  <Column fill>
    <Row justifyContent='space-between' alignItems='center'>
      <Text fontSize='regular' fontWeight='bold' color='text.secondary'>
        <FormattedMessage id='profile.teams' />
        <Condition match={teams.length > 0}>{`: ${teams.length}`}</Condition>
      </Text>
      <Condition match={isMyProject && teams.length > 0}>
        <Box alignItems='center'>
          <Button variant='link' size='micro'>
            <Text fontSize='medium' color='currentColor'>
              <FormattedMessage id='profile.manage' />
            </Text>
          </Button>
          <Layout width={64} />
          <ButtonCreateTeam user={ownerUser} />
        </Box>
      </Condition>
    </Row>
    <Layout flexBasis={30} flexShrink={0} />
    <Condition match={teams.length > 0}>
      <Grid gap={32} gridWrap='large' justifyContent='space-between'>
        {teams.map((team) => (
          <CardTeam key={team.id} team={team} cardSize='large' />
        ))}
      </Grid>
    </Condition>
    <Condition match={teams.length === 0}>
      <Column justifyContent='center' alignItems='center' height={284}>
        <Text
          fontSize='semiLarge'
          fontWeight='semiBold'
          color='text.gray'
          textAlign='center'
          opacity={0.6}
        >
          <FormattedMessage id='profile.no_teams_yet' />
        </Text>
        <Condition match={isMyProject}>
          <Layout flexBasis={30} flexShrink={0} />
          <ButtonCreateTeam user={ownerUser} />
          <Layout flexBasis={20} flexShrink={0} />
          <NextLink path={routes.teams}>
            <Button variant='link' size='micro'>
              <Text fontSize='normal' color='currentColor'>
                <FormattedMessage id='profile.find_team' />
              </Text>
            </Button>
          </NextLink>
        </Condition>
      </Column>
    </Condition>
  </Column>
)
