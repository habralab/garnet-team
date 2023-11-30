import React                     from 'react'
import { FC }                    from 'react'
import { FormattedMessage }      from 'react-intl'
import { useState }              from 'react'

import { Button }                from '@ui/button'
import { Condition }             from '@ui/condition'
import { Layout }                from '@ui/layout'
import { Column }                from '@ui/layout'
import { Box }                   from '@ui/layout'
import { Text }                  from '@ui/text'

import { NotificationItem }      from './notification-item'
import { NotificationListProps } from './notification-list.interfaces'

export const NotificationList: FC<NotificationListProps> = ({ notifications }) => {
  const [showAll, setShowAll] = useState<boolean>(false)

  const handleShowAll = () => setShowAll(true)

  return (
    <>
      <Condition match={notifications.length === 0}>
        <Box height={125} alignItems='center' justifyContent='center'>
          <Text
            fontSize='semiLarge'
            fontWeight='semiBold'
            color='text.gray'
            textAlign='center'
            opacity={0.6}
          >
            <FormattedMessage id='header.notifications.no_notifications_yet' />
          </Text>
        </Box>
      </Condition>
      <Condition match={notifications.length !== 0}>
        <Column fill id='custom-scrollbar' maxHeight={476} gap={20} overflow='auto'>
          {notifications.slice(0, showAll ? -1 : 3).map((notification) => (
            <NotificationItem key={notification.id} {...notification} />
          ))}
        </Column>
        <Condition match={!showAll}>
          <Layout flexBasis={50} flexShrink={0} />
          <Box fill justifyContent='flex-end'>
            <Button variant='link' size='micro' onClick={handleShowAll}>
              <Text fontSize='medium' color='currentColor'>
                <FormattedMessage id='header.notifications.watch_all' />
              </Text>
            </Button>
            <Layout flexBasis={16} flexShrink={0} />
          </Box>
        </Condition>
      </Condition>
    </>
  )
}
