import React                     from 'react'
import { FC }                    from 'react'
import { useIntl }               from 'react-intl'

import { Settings2Icon } from '@ui/icon'

import { CardWithLink }          from '../card-with-link'
import { CardTeamProps }         from './card-team.interfaces'

export const CardTeam: FC<CardTeamProps> = ({ team, cardSize = 'large' }) => {
  const { formatMessage } = useIntl()

  const countUsers = team.countUsers || 0
  const countProjects = team.countProjects || 0

  const mockAvatarsTeams = Array.from({ length: countProjects }, () => team.avatarUrl || '')

  return (
    <CardWithLink
      avatarUrl={team.avatarUrl}
      name={team.name}
      countItems={countProjects}
      countItemsWord={formatMessage(
        { id: 'shared_ui.card.project_words' },
        { count: countProjects }
      )}
      countPeopleWord={formatMessage({ id: 'shared_ui.card.people_words' }, { count: countUsers })}
      itemsAvatars={mockAvatarsTeams}
      itemsAvatarsShape='circle'
      cardSize={cardSize}
      url={`/team/${team.id}`}
      itemsIcon={<Settings2Icon width={14} height={14} color='accentPressed' />}
    />
  )
}
