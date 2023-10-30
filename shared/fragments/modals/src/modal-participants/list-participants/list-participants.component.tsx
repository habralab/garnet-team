import React                     from 'react'
import { Fragment }              from 'react'
import { FC }                    from 'react'
import { FormattedMessage }      from 'react-intl'
import { useState }              from 'react'
import { useIntl }               from 'react-intl'

import { Avatar }                from '@ui/avatar'
import { Button }                from '@ui/button'
import { Condition }             from '@ui/condition'
import { ClearIcon }             from '@ui/icon'
import { Search }                from '@ui/input'
import { Box }                   from '@ui/layout'
import { Column }                from '@ui/layout'
import { Row }                   from '@ui/layout'
import { Layout }                from '@ui/layout'
import { Text }                  from '@ui/text'

import { ListParticipantsProps } from './list-participants.interfaces'

const isIncludes = (value?: string, search?: string) =>
  Boolean(value?.toLowerCase().includes(search?.toLocaleLowerCase() || ''))

export const ListParticipants: FC<ListParticipantsProps> = ({
  isMyTeam = false,
  ownerUser,
  participants = [],
}) => {
  const [searchValue, setSearchValue] = useState<string>('')

  const { formatMessage } = useIntl()

  const filteredParticipants = participants.filter(
    ({ id, userName }) => id !== ownerUser?.id && isIncludes(userName, searchValue)
  )

  const handleMakeOwner = () => {
    /** @todo make owner request */
  }

  const handleRemoveUser = () => {
    /** @todo remove user request */
  }

  return (
    <>
      <Search
        value={searchValue}
        onChange={setSearchValue}
        placeholder={formatMessage({ id: 'shared_ui.modal.search_by_participants' })}
        showDropdown={false}
      />
      <Layout flexBasis={50} flexShrink={0} />
      <Column fill id='custom-scrollbar' maxHeight={412} overflow='auto'>
        <Condition match={isIncludes(ownerUser?.userName, searchValue)}>
          <Row>
            <Avatar
              image={ownerUser?.avatarUrl}
              size={124}
              title={ownerUser?.userName}
              url={`/user/${ownerUser?.id}`}
            />
            <Layout flexBasis={24} flexShrink={0} />
            <Column width='100%' height='auto' justifyContent='center'>
              <Text fontSize='regular' fontWeight='bold' color='text.secondary'>
                {ownerUser?.userName}
              </Text>
              <Layout flexBasis={10} flexShrink={0} />
              <Text fontSize='normal' color='text.gray'>
                <FormattedMessage id='shared_ui.modal.owner' />
              </Text>
            </Column>
          </Row>
        </Condition>
        <Layout flexBasis={20} flexShrink={0} />
        <Condition match={filteredParticipants.length === 0}>
          <Box justifyContent='center'>
            <Text fontSize='regular' fontWeight='semiBold' color='text.gray' textAlign='center'>
              <FormattedMessage id='shared_ui.modal.users_not_found' />
            </Text>
          </Box>
        </Condition>
        <Condition match={filteredParticipants.length > 0}>
          {filteredParticipants.map(({ id, avatarUrl, userName }) => (
            <Fragment key={id}>
              <Row>
                <Avatar image={avatarUrl} size={124} title={userName} url={`/user/${id}`} />
                <Layout flexBasis={24} flexShrink={0} />
                <Column width='100%' height='auto' justifyContent='center'>
                  <Text fontSize='regular' fontWeight='bold' color='text.secondary'>
                    {userName}
                  </Text>
                  <Condition match={isMyTeam}>
                    <Layout flexBasis={10} flexShrink={0} />
                    <Box>
                      <Button variant='link' size='micro' onClick={handleMakeOwner}>
                        <Text fontSize='normal' color='currentColor'>
                          <FormattedMessage id='shared_ui.modal.make_owner' />
                        </Text>
                      </Button>
                    </Box>
                  </Condition>
                </Column>
                <Layout flexBasis={16} flexShrink={0} />
                <Condition match={isMyTeam}>
                  <Box width={120} alignItems='center'>
                    <Button variant='link' size='micro' onClick={handleRemoveUser}>
                      <ClearIcon width={32} height={32} color='secondary' />
                    </Button>
                  </Box>
                </Condition>
              </Row>
              <Layout flexBasis={20} flexShrink={0} />
            </Fragment>
          ))}
        </Condition>
      </Column>
    </>
  )
}
