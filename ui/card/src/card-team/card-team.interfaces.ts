import { Team }     from '@shared/data'

import { CardSize } from '../card.interfaces'

export interface CardTeamProps {
  team: Team
  cardSize?: CardSize
}
