import { CardSize }   from './card.interfaces'
import { SizeConfig } from './card.interfaces'

export const sizeConfig: Record<CardSize, SizeConfig> = {
  large: {
    size: 284,
    fontSize: 'regular',
    statisticGap: 20,
    avatarGroupShow: true,
  },
  normal: {
    size: 180,
    fontSize: 'medium',
    statisticGap: 10,
    avatarGroupShow: true,
  },
  small: {
    size: 150,
    fontSize: 'semiMedium',
    statisticGap: 8,
    avatarGroupShow: false,
  },
}
