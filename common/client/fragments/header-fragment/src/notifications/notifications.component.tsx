import React                   from 'react'
import { FC }                  from 'react'
import { FormattedMessage }    from 'react-intl'
import { useState }            from 'react'
import { useLayer }            from 'react-laag'

import { Background }          from '@ui/background'
import { Condition }           from '@ui/condition'
import { BellActiveIcon }      from '@ui/icon'
import { BellIcon }            from '@ui/icon'
import { Layout }              from '@ui/layout'
import { Row }                 from '@ui/layout'
import { Column }              from '@ui/layout'
import { Box }                 from '@ui/layout'
import { Text }                from '@ui/text'
import { useHover }            from '@shared/utils'

import { useGetNotifications } from '../data'

export const Notifications: FC = () => {
  const [hover, hoverProps] = useHover()
  const [popoverOpen, setPopoverOpen] = useState(false)

  const togglePopoverOpen = () => setPopoverOpen(!popoverOpen)

  const { notifications, refetch } = useGetNotifications()

  const { renderLayer, layerProps, triggerProps, triggerBounds } = useLayer({
    isOpen: popoverOpen,
    placement: 'bottom-center',
    triggerOffset: 30,
    overflowContainer: false,
    auto: true,
    onOutsideClick: togglePopoverOpen,
  })

  return (
    <>
      <Box
        color={hover ? 'text.accentHover' : 'text.primary'}
        style={{ cursor: 'pointer' }}
        {...hoverProps}
        {...triggerProps}
        onClick={togglePopoverOpen}
      >
        <Condition match={notifications.length === 0}>
          <BellIcon width={24} height={24} />
        </Condition>
        <Condition match={notifications.length !== 0}>
          <BellActiveIcon width={24} height={24} />
        </Condition>
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
              padding='32px'
              flexDirection='column'
              position='relative'
            >
              <Text fontSize='semiLarge' fontWeight='semiBold' color='text.secondary'>
                <FormattedMessage id='header.notifications.notifications' />
              </Text>
              <Layout flexBasis={20} flexShrink={0} />
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
                <Column fill id='custom-scrollbar' maxHeight={492} gap={20} overflow='auto'>
                  {notifications.map((item) => (
                    <Row height={64}>
                      <Text fontSize='medium' color='text.secondary'>
                        {item.title}. {item.body}
                      </Text>
                      <Layout flexBasis={14} flexShrink={0} />
                    </Row>
                  ))}
                </Column>
              </Condition>
            </Background>
            <Layout flexBasis={24} flexShrink={0} />
          </Box>
        )}
      </Condition>
    </>
  )
}
