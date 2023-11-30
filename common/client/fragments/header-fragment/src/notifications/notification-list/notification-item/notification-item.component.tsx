import React                           from 'react'
import { FC }                          from 'react'
import { FormattedDate }               from 'react-intl'
import { FormattedMessage }            from 'react-intl'
import { FormattedRelativeTime }       from 'react-intl'
import { useEffect }                   from 'react'
import { useState }                    from 'react'

import { Avatar }                      from '@ui/avatar'
import { Button }                      from '@ui/button'
import { Condition }                   from '@ui/condition'
import { Layout }                      from '@ui/layout'
import { Row }                         from '@ui/layout'
import { Column }                      from '@ui/layout'
import { Box }                         from '@ui/layout'
import { NextLink }                    from '@ui/link'
import { Text }                        from '@ui/text'
import { getDaysAgo }                  from '@shared/utils'

import { ButtonDelete }                from './button-delete'
import { ButtonsDecide }               from './buttons-decide'
import { NotificationItemProps }       from './notification-item.interfaces'
import { getPathFromNotificationType } from '../../../helpers'
import { decideTypes }                 from './notification-item.constants'

export const NotificationItem: FC<NotificationItemProps> = ({
  body,
  createdAt,
  id,
  linkedEntityId,
  type,
  quotedEntities,
}) => {
  const [notificationText, setNotificationText] = useState<string | undefined>(body)

  useEffect(() => {
    let newNotificationText = body

    quotedEntities?.forEach(({ quote }) => {
      if (!quote) return

      newNotificationText = newNotificationText?.replace(quote, `<b>${quote}</b>`)
    })

    setNotificationText(newNotificationText)
  }, [body, quotedEntities])

  const daysAgo = getDaysAgo(createdAt || '')

  const isDecideType = Boolean(type && decideTypes.includes(type))

  return (
    <Row height={62} alignItems='center'>
      <Avatar image={quotedEntities[0].avatarUrl} shape='square' size={62} />
      <Layout flexBasis={10} flexShrink={0} />
      <Column fill flexShrink={1}>
        <Condition match={daysAgo <= 1}>
          <Text fontSize='normal' color='text.gray' textTransform='capitalize'>
            <FormattedRelativeTime numeric='auto' unit='day' value={-daysAgo} />
          </Text>
        </Condition>
        <Condition match={daysAgo > 1}>
          <Text fontSize='normal' color='text.gray'>
            <FormattedDate value={createdAt || ''} day='2-digit' month='short' />
          </Text>
        </Condition>
        <Text
          fontSize='medium'
          lineHeight='big'
          color='text.secondary'
          overflow='hidden'
          maxHeight='3em'
          display='inline'
          title={body}
          dangerouslySetInnerHTML={{ __html: notificationText || '' }}
        />
      </Column>
      <Layout flexBasis={10} flexShrink={0} />
      <Box
        flexBasis={isDecideType ? 250 : 190}
        flexShrink={0}
        alignItems='center'
        justifyContent='flex-end'
      >
        <Condition match={!isDecideType}>
          <NextLink path={getPathFromNotificationType(type, linkedEntityId)}>
            <Button variant='link' size='micro'>
              <Text fontSize='normal' color='currentColor'>
                <FormattedMessage id='header.notifications.more_detailed' />
              </Text>
            </Button>
          </NextLink>
          <Layout flexBasis={28} flexShrink={0} />
        </Condition>
        <Condition match={isDecideType}>
          <ButtonsDecide linkedRequestId={linkedEntityId} type={type} />
          <Layout flexBasis={14} flexShrink={0} />
        </Condition>
        <ButtonDelete id={id} />
        <Layout flexBasis={16} flexShrink={0} />
      </Box>
    </Row>
  )
}
