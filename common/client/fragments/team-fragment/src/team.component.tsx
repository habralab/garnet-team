import React                from 'react'
import { FC }               from 'react'
import { FormattedMessage } from 'react-intl'
import { useState }         from 'react'
import { useIntl }          from 'react-intl'

import { CardTeam }         from '@ui/card'
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
import { getUniqueTags }    from '@shared/helpers'

import { useGetTeams }      from './data'

export const Team: FC = () => {
  const [selectedTags, setSelectedTags] = useState<string[]>([])
  const [searchValue, setSearchValue] = useState<string>('')
  const { formatMessage } = useIntl()

  const { teams } = useGetTeams({ search: '', skip: 0, tags: [], take: 0 })

  const uniqueTags = getUniqueTags(teams)

  const handleAddTag = (tag: string) => {
    setSelectedTags(selectedTags.concat([tag]))
  }

  const handleRemoveTag = (tag: string) => () => {
    setSelectedTags(selectedTags.filter((item) => item !== tag))
  }

  /** @todo search params to url query */
  const filteredTeams = teams.filter(
    (team) =>
      selectedTags.some((tag) => team.tags?.includes(tag)) ||
      (searchValue.length > 2 && team.name?.toLowerCase().includes(searchValue.toLocaleLowerCase()))
  )

  return (
    <Column fill marginBottom={32}>
      <Title>
        <FormattedMessage id='team.find_team' />
      </Title>
      <Box justifyContent='center'>
        <Column fill maxWidth={1074}>
          <Condition match={teams.length > 0}>
            <Search
              value={searchValue}
              onChange={setSearchValue}
              options={uniqueTags.filter((tag) => !selectedTags.includes(tag))}
              onChangeOptions={handleAddTag}
              placeholder={formatMessage({ id: 'team.enter_tag_name_description' })}
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
            <Condition match={teams.length === 0}>
              <Box height={52} justifyContent='center' alignItems='center'>
                <Text fontSize='semiLarge' color='text.gray'>
                  <FormattedMessage id='team.select_necessary_skills' />
                </Text>
              </Box>
            </Condition>
            <Condition match={teams.length > 0}>
              <Condition match={filteredTeams.length === 0}>
                <Layout flexBasis={60} flexShrink={0} />
                <Box fill width={350} justifyContent='center' alignItems='center'>
                  <Text
                    fontSize='semiLarge'
                    fontWeight='semiBold'
                    color='text.gray'
                    whiteSpace='pre-wrap'
                    textAlign='center'
                  >
                    <FormattedMessage id='team.select_necessary_skills' />
                  </Text>
                </Box>
                <Layout flexBasis={60} flexShrink={0} />
              </Condition>
              <Condition match={filteredTeams.length > 0}>
                <Text fontSize='regular' fontWeight='bold' color='text.secondary'>
                  <FormattedMessage id='team.teams_found' />
                  {`: ${filteredTeams.length}`}
                </Text>
                <Layout flexBasis={42} flexShrink={0} />
                <Grid gap={22} gridWrap='small' justifyContent='space-between'>
                  {filteredTeams.map((team) => (
                    <CardTeam key={team.id} team={team} cardSize='small' />
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
