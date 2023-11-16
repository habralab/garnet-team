import React             from 'react'
import { FC }            from 'react'
import { useIntl }       from 'react-intl'

import { Settings2Icon } from '@ui/icon'
import { routes }        from '@shared/routes'

import { CardWithLink }  from '../card-with-link'
import { CardTeamProps } from './card-team.interfaces'

export const CardTeam: FC<CardTeamProps> = ({ team, cardSize = 'large' }) => {
  const { formatMessage } = useIntl()

  const teamUsers = team.teamParticipants || []
  const countProjects = team.projectCount || 0

  const avatarsParticipants = (teamUsers?.map((user) => user.avatarUrl) || []) as string[]

  return (
    <CardWithLink
      avatarUrl={team.avatarUrl}
      name={team.name}
      countItems={countProjects}
      countItemsWord={formatMessage(
        { id: 'shared_ui.card.project_words' },
        { count: countProjects }
      )}
      countPeopleWord={formatMessage(
        { id: 'shared_ui.card.people_words' },
        { count: teamUsers.length }
      )}
      itemsAvatars={avatarsParticipants}
      itemsAvatarsShape='circle'
      cardSize={cardSize}
      url={`${routes.teams}/${team.id}`}
      itemsIcon={<Settings2Icon width={14} height={14} color='accentPressed' />}
    />
  )
}
