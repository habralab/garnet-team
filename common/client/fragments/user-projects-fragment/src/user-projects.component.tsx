import React                    from 'react'
import { FC }                   from 'react'
import { FormattedMessage }     from 'react-intl'
import { useState }             from 'react'

import { Condition }            from '@ui/condition'
import { Box }                  from '@ui/layout'
import { Column }               from '@ui/layout'
import { Row }                  from '@ui/layout'
import { Layout }               from '@ui/layout'
import { Tag }                  from '@ui/tag'
import { Text }                 from '@ui/text'
import { Title }                from '@ui/title'
import { WrapperWhite }         from '@ui/wrapper'

import { ListProjects }         from './list-projects'
import { useUserProjectsState } from './hooks'

export const UserProjects: FC = () => {
  const [selectedTags, setSelectedTags] = useState<string[]>([])

  const { projects, isMyProfile, uniqueTags } = useUserProjectsState(setSelectedTags)

  const handleClickTag = (tag: string) => () => {
    if (selectedTags.includes(tag)) {
      setSelectedTags(selectedTags.filter((item) => item !== tag))

      return
    }

    setSelectedTags(selectedTags.concat([tag]))
  }

  const filteredProjects = projects.filter((project) =>
    selectedTags.some((tag) => project.tags?.includes(tag)))

  return (
    <Column fill marginBottom={32}>
      <Title hasBack>
        <Condition match={isMyProfile}>
          <FormattedMessage id='user_projects.my_projects' />
        </Condition>
        <Condition match={!isMyProfile}>
          <FormattedMessage id='user_projects.projects' />
        </Condition>
        <Condition match={projects.length > 0}>{`: ${projects.length}`}</Condition>
      </Title>
      <Box justifyContent='center'>
        <Column fill maxWidth={1074}>
          <Condition match={projects.length > 0}>
            <Text fontSize='preLarge' fontWeight='bold' color='text.secondary'>
              <FormattedMessage id='user_projects.filter_by_skills' />
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
            <Condition match={projects.length === 0}>
              <Box height={52} justifyContent='center' alignItems='center'>
                <Text fontSize='semiLarge' fontWeight='semiBold' color='text.gray' opacity={0.6}>
                  <Condition match={isMyProfile}>
                    <FormattedMessage id='user_projects.you_do_not_have_projects' />
                  </Condition>
                  <Condition match={!isMyProfile}>
                    <FormattedMessage id='user_projects.no_projects' />
                  </Condition>
                </Text>
              </Box>
            </Condition>
            <Condition match={projects.length > 0}>
              <ListProjects projects={filteredProjects} />
            </Condition>
          </WrapperWhite>
        </Column>
      </Box>
    </Column>
  )
}
