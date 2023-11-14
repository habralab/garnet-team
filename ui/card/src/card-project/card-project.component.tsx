import React                 from 'react'
import { FC }                from 'react'
import { useIntl }           from 'react-intl'

import { UserGroupIcon }     from '@ui/icon'
import { routes }            from '@shared/routes'

import { CardWithLink }      from '../card-with-link'
import { CardProjectsProps } from './card-project.interfaces'

export const CardProject: FC<CardProjectsProps> = ({ project, cardSize = 'large' }) => {
  const { formatMessage } = useIntl()

  const countUsers = project.countUsers || 0
  const countTeams = project.countTeams || 0

  const mockAvatarsTeams = Array.from({ length: countTeams }, () => project.avatarUrl || '')

  return (
    <CardWithLink
      avatarUrl={project.avatarUrl}
      name={project.projectName}
      countItems={countTeams}
      countItemsWord={formatMessage({ id: 'shared_ui.card.team_words' }, { count: countTeams })}
      countPeopleWord={formatMessage({ id: 'shared_ui.card.people_words' }, { count: countUsers })}
      itemsAvatars={mockAvatarsTeams}
      itemsAvatarsShape='square'
      cardSize={cardSize}
      url={`${routes.projects}/${project.id}`}
      itemsIcon={<UserGroupIcon width={14} height={14} color='accentPressed' />}
    />
  )
}
