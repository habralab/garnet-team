import React                from 'react'
import { FC }               from 'react'
import { FormattedMessage } from 'react-intl'

import { Avatar }           from '@ui/avatar'
import { Background }       from '@ui/background'
import { Box }              from '@ui/layout'
import { Column }           from '@ui/layout'
import { Row }              from '@ui/layout'
import { Layout }           from '@ui/layout'
import { Tag }              from '@ui/tag'
import { Text }             from '@ui/text'
import { Title }            from '@ui/title'

import { Team }             from './data'
import { mockUser }         from './data'

const getTeamsUniqueTags = (teams: Team[]): (string | undefined)[] => {
  const allTeamsTags = teams.map((team) => team.tags).flat()

  const setUniqueTags = new Set<string | undefined>(allTeamsTags)

  setUniqueTags.delete(undefined)

  return Array.from(setUniqueTags)
}

export const UserTeams: FC = () => {
  const teams = mockUser.teamsListByUser?.teams || []

  const uniqueTags = getTeamsUniqueTags(teams)

  return (
    <Column fill>
      <Title hasBack>
        <FormattedMessage id='user_teams.my_teams' />: {teams.length}
      </Title>
      <Box fill justifyContent='center'>
        <Column fill maxWidth={1074}>
          <Text fontSize='preLarge' fontWeight='bold' color='text.secondary'>
            <FormattedMessage id='user_teams.filter_by_skills' />
          </Text>
          <Layout flexBasis={20} flexShrink={0} />
          <Row flexWrap='wrap' style={{ gap: 10 }}>
            {uniqueTags.map((tag) => (
              <Tag>{tag}</Tag>
            ))}
          </Row>
          <Layout flexBasis={32} flexShrink={0} />
          <Background fill color='white' borderRadius='medium' boxShadow='black'>
            <Layout flexBasis={32} />
            <Column fill>
              <Layout flexBasis={32} />
              <Row
                display='grid'
                style={{ gap: 22, gridTemplateColumns: 'repeat(auto-fill, 150px)' }}
                justifyContent='space-between'
              >
                {teams.map(({ avatarUrl, name, id }) => (
                  <Column height='max-content' alignItems='center' key={id}>
                    <Avatar
                      image={avatarUrl}
                      shape='square'
                      size={150}
                      title={name}
                      url={`/team/${id}`}
                    />
                    <Layout flexBasis={10} flexShrink={0} />
                    <Text fontSize='medium' fontWeight='bold' color='text.secondary'>
                      {name}
                    </Text>
                  </Column>
                ))}
              </Row>
              <Layout flexBasis={32} />
            </Column>
            <Layout flexBasis={32} />
          </Background>
        </Column>
      </Box>
    </Column>
  )
}
