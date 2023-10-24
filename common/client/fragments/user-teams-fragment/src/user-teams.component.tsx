import React                from 'react'
import { FC }               from 'react'
import { FormattedMessage } from 'react-intl'
import { useRouter }        from 'next/router'
import { useEffect }        from 'react'
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
import { mockAuthUserId }   from '@shared/data'
import { getMockUser }      from '@shared/data'
import { getUniqueTags }    from '@shared/helpers'

import { ListTeams }        from './list-teams/list-teams.component'

export const UserTeams: FC = () => {
  const [isMyProfile, setIsMyProfile] = useState(false)
  const [selectedTags, setSelectedTags] = useState<string[]>([])

  const router = useRouter()
  const userData = getMockUser(String(router.query.id) || '')

  const user = userData?.user
  const teams = userData?.teams || []
  const uniqueTags = getUniqueTags(teams)

  useEffect(() => {
    if (user?.id === mockAuthUserId) setIsMyProfile(true)

    setSelectedTags(uniqueTags)

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [user])

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
        <Condition match={isMyProfile}>
          <FormattedMessage id='user_teams.my_teams' />
        </Condition>
        <Condition match={!isMyProfile}>
          <FormattedMessage id='user_teams.teams' />
        </Condition>
        <Condition match={teams.length > 0}>{`: ${teams.length}`}</Condition>
      </Title>
      <Box justifyContent='center'>
        <Column fill maxWidth={1074}>
          <Condition match={teams.length > 0}>
            <Text fontSize='preLarge' fontWeight='bold' color='text.secondary'>
              <FormattedMessage id='user_teams.filter_by_skills' />
            </Text>
            <Layout flexBasis={20} flexShrink={0} />
            <Row flexWrap='wrap' style={{ gap: 10 }}>
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
                <Text fontSize='semiLarge' color='text.gray'>
                  <Condition match={isMyProfile}>
                    <FormattedMessage id='user_teams.you_do_not_have_teams' />
                  </Condition>
                  <Condition match={!isMyProfile}>
                    <FormattedMessage id='user_teams.no_teams' />
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
