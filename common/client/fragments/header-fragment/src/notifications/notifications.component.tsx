import React                   from 'react'
import { FC }                  from 'react'
import { FormattedMessage }    from 'react-intl'
import { useState }            from 'react'
import { useLayer }            from 'react-laag'

import { Background }          from '@ui/background'
import { Condition }           from '@ui/condition'
import { Layout }              from '@ui/layout'
import { Column }              from '@ui/layout'
import { Box }                 from '@ui/layout'
import { Text }                from '@ui/text'

import { NotificationIcon }    from './notification-icon'
import { NotificationList }    from './notification-list/notification-list.component'
import { useGetNotifications } from '../data'

export const Notifications: FC = () => {
  const [popoverOpen, setPopoverOpen] = useState<boolean>(false)

  const togglePopoverOpen = () => setPopoverOpen(!popoverOpen)

  const { notifications } = useGetNotifications()

  const { renderLayer, layerProps, triggerProps } = useLayer({
    isOpen: popoverOpen,
    placement: 'bottom-center',
    triggerOffset: 30,
    overflowContainer: false,
    auto: true,
    onOutsideClick: togglePopoverOpen,
  })

  return (
    <>
      <Box {...triggerProps} style={{ cursor: 'pointer' }} onClick={togglePopoverOpen}>
        <NotificationIcon notificationCount={notifications.length} />
      </Box>
      <Condition match={popoverOpen}>
        {renderLayer(
          <Box fill maxWidth={724} {...layerProps}>
            <Background
              width='100%'
              height='fit-content'
              color='white'
              borderRadius='medium'
              border='lightGrayForty'
              boxShadow='blackTwenty'
              position='relative'
            >
              <Layout flexBasis={32} flexShrink={0} />
              <Column fill>
                <Layout flexBasis={32} flexShrink={0} />
                <Text fontSize='semiLarge' fontWeight='semiBold' color='text.secondary'>
                  <FormattedMessage id='header.notifications.notifications' />
                </Text>
                <Layout flexBasis={20} flexShrink={0} />
                <NotificationList notifications={notifications} />
                <Layout flexBasis={32} flexShrink={0} />
              </Column>
              <Layout flexBasis={16} flexShrink={0} />
            </Background>
            <Layout flexBasis={24} flexShrink={0} />
          </Box>
        )}
      </Condition>
    </>
  )
}
