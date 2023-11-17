import React                from 'react'
import { FC }               from 'react'
import { FormattedMessage } from 'react-intl'
import { useEffect }        from 'react'
import { useMemo }          from 'react'
import { useState }         from 'react'
import { useIntl }          from 'react-intl'

import { CardProject }      from '@ui/card'
import { Condition }        from '@ui/condition'
import { Search }           from '@ui/input'
import { Box }              from '@ui/layout'
import { Column }           from '@ui/layout'
import { Row }              from '@ui/layout'
import { Grid }             from '@ui/layout'
import { Layout }           from '@ui/layout'
import { Tag }              from '@ui/tag'
import { Text }             from '@ui/text'
import { Title }            from '@ui/title'
import { WrapperWhite }     from '@ui/wrapper'
import { getUniqueTags }    from '@shared/utils'
import { useDebounce }      from '@shared/utils'

import { useGetProjects }   from './data'

export const Project: FC = () => {
  const [uniqueTags, setUniqueTags] = useState<string[]>([])
  const [selectedTags, setSelectedTags] = useState<string[]>([])
  const [searchValue, setSearchValue] = useState<string>('')
  const { formatMessage } = useIntl()

  const filters = useMemo(
    () => ({ search: searchValue, tags: selectedTags, skip: 0, take: 0 }),
    [searchValue, selectedTags]
  )
  const debounceFilters = useDebounce(filters)

  const { projects } = useGetProjects(debounceFilters)

  useEffect(() => {
    if (projects.length > 0 && uniqueTags.length === 0) {
      setUniqueTags(getUniqueTags(projects))
    }

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [projects])

  const handleAddTag = (tag: string) => {
    setSelectedTags(selectedTags.concat([tag]))
  }

  const handleRemoveTag = (tag: string) => () => {
    setSelectedTags(selectedTags.filter((item) => item !== tag))
  }

  return (
    <Column fill marginBottom={32}>
      <Title>
        <FormattedMessage id='project.find_project' />
      </Title>
      <Box justifyContent='center'>
        <Column fill maxWidth={1074}>
          <Condition match={projects.length > 0}>
            <Search
              value={searchValue}
              onChange={setSearchValue}
              options={uniqueTags.filter((tag) => !selectedTags.includes(tag))}
              onChangeOptions={handleAddTag}
              placeholder={formatMessage({ id: 'project.enter_tag_name_description' })}
            />
            <Layout flexBasis={20} flexShrink={0} />
            <Row flexWrap='wrap' gap={10}>
              {selectedTags.map((tag) => (
                <Tag key={tag} close variant='primary' onClick={handleRemoveTag(tag)}>
                  {tag}
                </Tag>
              ))}
            </Row>
            <Layout flexBasis={32} flexShrink={0} />
          </Condition>
          <WrapperWhite>
            <Condition match={projects.length === 0}>
              <Box height={52} justifyContent='center' alignItems='center'>
                <Text fontSize='semiLarge' color='text.gray'>
                  <FormattedMessage id='project.select_necessary_skills' />
                </Text>
              </Box>
            </Condition>
            <Condition match={projects.length > 0}>
              <Condition match={projects.length === 0}>
                <Layout flexBasis={60} flexShrink={0} />
                <Box fill width={350} justifyContent='center' alignItems='center'>
                  <Text
                    fontSize='semiLarge'
                    fontWeight='semiBold'
                    color='text.gray'
                    whiteSpace='pre-wrap'
                    textAlign='center'
                  >
                    <FormattedMessage id='project.select_necessary_skills' />
                  </Text>
                </Box>
                <Layout flexBasis={60} flexShrink={0} />
              </Condition>
              <Condition match={projects.length > 0}>
                <Text fontSize='regular' fontWeight='bold' color='text.secondary'>
                  <FormattedMessage id='project.projects_found' />
                  {`: ${projects.length}`}
                </Text>
                <Layout flexBasis={42} flexShrink={0} />
                <Grid gap={22} gridWrap='small' justifyContent='space-between'>
                  {projects.map((project) => (
                    <CardProject key={project.id} project={project} cardSize='small' />
                  ))}
                </Grid>
              </Condition>
            </Condition>
          </WrapperWhite>
        </Column>
      </Box>
    </Column>
  )
}
