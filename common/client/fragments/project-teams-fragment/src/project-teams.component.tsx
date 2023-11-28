import React                from 'react'
import { FC }               from 'react'
import { FormattedMessage } from 'react-intl'
import { useState }         from 'react'

import { Condition }        from '@ui/condition'
import { Box }              from '@ui/layout'
import { Column }           from '@ui/layout'
import { Row }              from '@ui/layout'
import { Layout }           from '@ui/layout'
import { Tag }              from '@ui/tag'
import { Text }             from '@ui/text'
import { Title }            from '@ui/title'
import { WrapperWhite }     from '@ui/wrapper'

import { JoinRequests }     from './join-requests/join-requests.component'
import { ListTeams }        from './list-teams'
import { useProjectState }  from './hooks'

export const UserTeams: FC = () => {
  const [selectedTags, setSelectedTags] = useState<string[]>([])

  const { project, teams, isMyProject, uniqueTags, joinRequests, refetch } =
    useProjectState(setSelectedTags)

  const handleClickTag = (tag: string) => () => {
    if (selectedTags.includes(tag)) {
      setSelectedTags(selectedTags.filter((item) => item !== tag))

      return
    }

    setSelectedTags(selectedTags.concat([tag]))
  }

  const filteredTeams = teams.filter((team) => selectedTags.some((tag) => team.tags?.includes(tag)))

  return (
    <Column fill marginBottom={32}>
      <Title hasBack>
        <FormattedMessage id='project_teams.teams' />
        {` «${project?.projectName}»`}
        <Condition match={teams.length > 0}>{`: ${teams.length}`}</Condition>
      </Title>
      <Box justifyContent='center'>
        <Column fill maxWidth={1074}>
          <Condition match={joinRequests.length > 0}>
            <WrapperWhite>
              <JoinRequests joinRequests={joinRequests} refetch={refetch} />
            </WrapperWhite>
            <Layout flexBasis={32} flexShrink={0} />
          </Condition>
          <Condition match={teams.length > 0}>
            <Text fontSize='preLarge' fontWeight='bold' color='text.secondary'>
              <FormattedMessage id='project_teams.filter_by_skills' />
            </Text>
            <Layout flexBasis={20} flexShrink={0} />
            <Row flexWrap='wrap' gap={10}>
              {uniqueTags.map((tag) => (
                <Tag
                  key={tag}
                  onClick={handleClickTag(tag)}
                  variant={selectedTags.includes(tag) ? 'primary' : 'secondary'}
                  close={selectedTags.includes(tag)}
                >
                  {tag}
                </Tag>
              ))}
            </Row>
            <Layout flexBasis={32} flexShrink={0} />
          </Condition>
          <WrapperWhite>
            <Condition match={teams.length === 0}>
              <Box height={52} justifyContent='center' alignItems='center'>
                <Text fontSize='semiLarge' fontWeight='semiBold' color='text.gray' opacity={0.6}>
                  <Condition match={isMyProject}>
                    <FormattedMessage id='project_teams.you_do_not_have_teams' />
                  </Condition>
                  <Condition match={!isMyProject}>
                    <FormattedMessage id='project_teams.no_teams' />
                  </Condition>
                </Text>
              </Box>
            </Condition>
            <Condition match={teams.length > 0}>
              <ListTeams teams={filteredTeams} />
            </Condition>
          </WrapperWhite>
        </Column>
      </Box>
    </Column>
  )
}
