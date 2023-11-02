import { Project }  from '@shared/data'

import { CardSize } from '../card.interfaces'

export interface CardProjectsProps {
  project: Project
  cardSize?: CardSize
}
