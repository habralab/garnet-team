import React                        from 'react'
import { FC }                       from 'react'
import { FormattedDate }            from 'react-intl'
import { FormattedRelativeTime }    from 'react-intl'
import { FormattedMessage }         from 'react-intl'

import { Avatar }                   from '@ui/avatar'
import { Button }                   from '@ui/button'
import { Condition }                from '@ui/condition'
import { HourglassIcon }            from '@ui/icon'
import { CheckIcon }                from '@ui/icon'
import { Box }                      from '@ui/layout'
import { Column }                   from '@ui/layout'
import { Row }                      from '@ui/layout'
import { Layout }                   from '@ui/layout'
import { Tag }                      from '@ui/tag'
import { Text }                     from '@ui/text'

import { UserRequestBlockProps }    from './user-request-block.interfaces'
import { useDecideTeamJoinRequest } from '../data'
import { getDaysAgo }               from './helpers'

export const UserRequestBlock: FC<UserRequestBlockProps> = ({ user, size = 'normal' }) => {
  const { id, avatarUrl, tags = [], userName, requestType, date = '' } = user

  const { decideTeamJoinRequest } = useDecideTeamJoinRequest()

  const daysAgo = getDaysAgo(date)

  const handleDecideTeamJoinRequest = (approve: boolean) => async () => {
    try {
      await decideTeamJoinRequest({ variables: { approve, id: user.requestId } })
    } catch (error) {
      /** @todo error notification */
    }
  }

  return (
    <Row>
      <Avatar
        key={id}
        image={avatarUrl}
        size={size === 'large' ? 124 : 74}
        title={userName}
        url={`/user/${id}`}
      />
      <Layout flexBasis={size === 'large' ? 24 : 16} flexShrink={0} />
      <Column fill justifyContent='center'>
        <Text
          fontSize={size === 'large' ? 'regular' : 'medium'}
          fontWeight='bold'
          color='text.secondary'
        >
          {userName}
        </Text>
        <Condition match={size === 'large' && tags.length > 0}>
          <Layout flexBasis={8} flexShrink={0} />
          <Row flexWrap='wrap' maxHeight={22} overflow='hidden' gap={10}>
            {tags.map((tag) => (
              <Tag key={tag} size='small'>
                {tag}
              </Tag>
            ))}
          </Row>
        </Condition>
        <Layout flexBasis={8} flexShrink={0} />
        <Box>
          <Button variant='link' size='micro' onClick={handleDecideTeamJoinRequest(false)}>
            <Text fontSize='normal' color='currentColor'>
              <FormattedMessage
                id={requestType === 'invite' ? 'profile.reject' : 'profile.cancel'}
              />
            </Text>
          </Button>
        </Box>
        <Layout flexBasis={8} flexShrink={0} />
        <Condition match={daysAgo <= 1}>
          <Text fontSize='normal' color='text.gray' textTransform='capitalize'>
            <FormattedRelativeTime numeric='auto' unit='day' value={-daysAgo} />
          </Text>
        </Condition>
        <Condition match={daysAgo > 1}>
          <Text fontSize='normal' color='text.gray'>
            <FormattedDate value={date} day='2-digit' month='short' />
          </Text>
        </Condition>
      </Column>
      <Layout flexBasis={16} flexShrink={0} />
      <Box alignItems='center' width={size === 'large' ? 120 : 32}>
        <Condition match={requestType === 'application'}>
          <Button variant='link' size='micro' onClick={handleDecideTeamJoinRequest(true)}>
            <CheckIcon width={32} height={32} />
          </Button>
        </Condition>
        <Condition match={requestType === 'invite'}>
          <HourglassIcon width={32} height={32} color='gray' />
        </Condition>
      </Box>
    </Row>
  )
}
