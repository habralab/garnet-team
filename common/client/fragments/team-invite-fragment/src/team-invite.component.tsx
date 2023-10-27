import React                from 'react'
import { FC }               from 'react'
import { FormattedMessage } from 'react-intl'
import { useState }         from 'react'
import { useIntl }          from 'react-intl'

import { Avatar }           from '@ui/avatar'
import { Condition }        from '@ui/condition'
import { Search }           from '@ui/input'
import { Box }              from '@ui/layout'
import { Column }           from '@ui/layout'
import { Row }              from '@ui/layout'
import { Layout }           from '@ui/layout'
import { Tag }              from '@ui/tag'
import { Text }             from '@ui/text'
import { Title }            from '@ui/title'
import { WrapperWhite }     from '@ui/wrapper'
import { mockUsers }        from '@shared/data'
import { getUniqueTags }    from '@shared/helpers'

export const TeamInvite: FC = () => {
  const [selectedTags, setSelectedTags] = useState<string[]>([])
  const [searchValue, setSearchValue] = useState<string>('')
  const { formatMessage } = useIntl()

  const users = mockUsers
  const uniqueTags = getUniqueTags(users)

  const handleAddTag = (tag: string) => {
    setSelectedTags(selectedTags.concat([tag]))
  }

  const handleRemoveTag = (tag: string) => () => {
    setSelectedTags(selectedTags.filter((item) => item !== tag))
  }

  const filteredUsers = users.filter(
    (user) =>
      selectedTags.some((tag) => user.tags?.includes(tag)) ||
      (searchValue.length > 2 &&
        user.userName?.toLowerCase().includes(searchValue.toLocaleLowerCase()))
  )

  return (
    <Column fill marginBottom={32}>
      <Title hasBack>
        <FormattedMessage id='team.invite.find_user' />
      </Title>
      <Box justifyContent='center'>
        <Column fill maxWidth={1074}>
          <Condition match={users.length > 0}>
            <Search
              value={searchValue}
              onChange={setSearchValue}
              options={uniqueTags.filter((tag) => !selectedTags.includes(tag))}
              onChangeOptions={handleAddTag}
              placeholder={formatMessage({ id: 'team.invite.enter_tag_name_surname' })}
            />
            <Layout flexBasis={20} flexShrink={0} />
            <Row flexWrap='wrap' style={{ gap: 10 }}>
              {selectedTags.map((tag) => (
                <Tag key={tag} close variant='primary' onClick={handleRemoveTag(tag)}>
                  {tag}
                </Tag>
              ))}
            </Row>
            <Layout flexBasis={32} flexShrink={0} />
          </Condition>
          <WrapperWhite>
            <Condition match={users.length === 0}>
              <Box height={52} justifyContent='center' alignItems='center'>
                <Text fontSize='semiLarge' color='text.gray'>
                  <FormattedMessage id='team.invite.select_necessary_skills' />
                </Text>
              </Box>
            </Condition>
            <Condition match={users.length > 0}>
              <Condition match={filteredUsers.length === 0}>
                <Layout flexBasis={60} flexShrink={0} />
                <Box fill width={350} justifyContent='center' alignItems='center'>
                  <Text
                    fontSize='semiLarge'
                    fontWeight='semiBold'
                    color='text.gray'
                    whiteSpace='pre-wrap'
                    textAlign='center'
                  >
                    <FormattedMessage id='team.invite.select_necessary_skills' />
                  </Text>
                </Box>
                <Layout flexBasis={60} flexShrink={0} />
              </Condition>
              <Condition match={filteredUsers.length > 0}>
                <Text fontSize='regular' fontWeight='bold' color='text.secondary'>
                  <FormattedMessage id='team.invite.users_found' />
                  {`: ${filteredUsers.length}`}
                </Text>
                <Layout flexBasis={42} flexShrink={0} />
                <Row
                  display='grid'
                  style={{ gap: 22, gridTemplateColumns: 'repeat(auto-fill, 150px)' }}
                  justifyContent='space-between'
                >
                  {filteredUsers.map(({ id, avatarUrl, userName }) => (
                    <Column alignItems='center'>
                      <Avatar
                        key={id}
                        size={150}
                        image={avatarUrl}
                        title={userName}
                        url={`/user/${id}`}
                      />
                      <Layout flexBasis={10} flexShrink={0} />
                      <Text
                        fontSize='medium'
                        fontWeight='bold'
                        textAlign='center'
                        color='text.secondary'
                      >
                        {userName}
                      </Text>
                    </Column>
                  ))}
                </Row>
              </Condition>
            </Condition>
          </WrapperWhite>
        </Column>
      </Box>
    </Column>
  )
}
