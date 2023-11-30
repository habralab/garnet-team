import React                     from 'react'
import { FC }                    from 'react'

import { Condition }             from '@ui/condition'
import { BellActiveIcon }        from '@ui/icon'
import { BellIcon }              from '@ui/icon'
import { Box }                   from '@ui/layout'
import { useHover }              from '@shared/utils'

import { NotificationIconProps } from './notification-icon.interfaces'

export const NotificationIcon: FC<NotificationIconProps> = ({ notificationCount }) => {
  const [hover, hoverProps] = useHover()

  return (
    <Box {...hoverProps} color={hover ? 'text.accentHover' : 'text.primary'}>
      <Condition match={notificationCount === 0}>
        <BellIcon width={24} height={24} />
      </Condition>
      <Condition match={notificationCount !== 0}>
        <BellActiveIcon width={24} height={24} />
      </Condition>
    </Box>
  )
}
