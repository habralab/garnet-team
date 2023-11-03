import { theme }      from '@ui/theme'

import { CardSize }   from './card.interfaces'
import { SizeConfig } from './card.interfaces'

export const sizeConfig: Record<CardSize, SizeConfig> = {
  large: {
    size: theme.sizes.large,
    fontSize: 'regular',
    statisticGap: 20,
    avatarGroupShow: true,
  },
  normal: {
    size: theme.sizes.normal,
    fontSize: 'medium',
    statisticGap: 10,
    avatarGroupShow: true,
  },
  small: {
    size: theme.sizes.small,
    fontSize: 'semiMedium',
    statisticGap: 8,
    avatarGroupShow: false,
  },
}
