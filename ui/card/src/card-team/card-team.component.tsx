import React                     from 'react'
import { FC }                    from 'react'
import { useIntl }               from 'react-intl'

import { Settings2Icon }         from '@ui/icon'
import { getDependOfNumberWord } from '@shared/helpers'

import { CardWithLink }          from '../card-with-link'
import { CardTeamProps }         from './card-team.interfaces'

export const CardTeam: FC<CardTeamProps> = ({ team, cardSize = 'large' }) => {
  const { formatMessage } = useIntl()

  const countUsers = team.countUsers || 0
  const countPeopleWord = `${countUsers} ${getDependOfNumberWord(
    countUsers,
    formatMessage({ id: 'shared_ui.card.people_words' }).split(' ')
  )}`

  const countProjects = team.countProjects || 0
  const countProjectsWord = `${countProjects} ${getDependOfNumberWord(
    countProjects,
    formatMessage({ id: 'shared_ui.card.project_words' }).split(' ')
  )}`

  const mockAvatarsTeams = Array.from({ length: countProjects }, () => team.avatarUrl || '')

  return (
    <CardWithLink
      avatarUrl={team.avatarUrl}
      name={team.name}
      countItems={countProjects}
      countItemsWord={countProjectsWord}
      countPeopleWord={countPeopleWord}
      itemsAvatars={mockAvatarsTeams}
      itemsAvatarsShape='circle'
      cardSize={cardSize}
      url={`/team/${team.id}`}
      itemsIcon={<Settings2Icon width={14} height={14} color='accentPressed' />}
    />
  )
}
