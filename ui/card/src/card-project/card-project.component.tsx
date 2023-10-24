import React                     from 'react'
import { FC }                    from 'react'
import { useRouter }             from 'next/router'
import { useIntl }               from 'react-intl'

import { UserGroupIcon }         from '@ui/icon'
import { getDependOfNumberWord } from '@utils/helpers'

import { Card }                  from '../card.component'
import { CardProjectsProps }     from './card-project.interfaces'

export const CardProject: FC<CardProjectsProps> = ({ project, cardSize = 'large' }) => {
  const router = useRouter()

  const { formatMessage } = useIntl()

  const countUsers = project.countUsers || 0
  const countPeopleWord = `${countUsers} ${getDependOfNumberWord(
    countUsers,
    formatMessage({ id: 'shared_ui.card.people_words' }).split(' ')
  )}`

  const countTeams = project.countTeams || 0
  const countTeamsWord = `${countTeams} ${getDependOfNumberWord(
    countTeams,
    formatMessage({ id: 'shared_ui.card.team_words' }).split(' ')
  )}`

  const mockAvatarsTeams = Array.from({ length: countTeams }, () => project.avatarUrl || '')

  const handleClick = () => router.push(`/project/${project.id}`)

  return (
    <Card
      avatarUrl={project.avatarUrl}
      name={project.name}
      countItems={countTeams}
      countItemsWord={countTeamsWord}
      countPeopleWord={countPeopleWord}
      itemsAvatars={mockAvatarsTeams}
      itemsAvatarsShape='square'
      cardSize={cardSize}
      onClick={handleClick}
      itemsIcon={<UserGroupIcon width={14} height={14} color='accentPressed' />}
    />
  )
}
