import React                     from 'react'
import { FC }                    from 'react'
import { useRouter }             from 'next/router'
import { useIntl }               from 'react-intl'

import { Settings2Icon }         from '@ui/icon'
import { getDependOfNumberWord } from '@utils/helpers'

import { Card }                  from '../card.component'
import { CardTeamProps }         from './card-team.interfaces'

export const CardTeam: FC<CardTeamProps> = ({ team, cardSize = 'large' }) => {
  const router = useRouter()

  const { formatMessage } = useIntl()

  const countUsers = team.countUsers || 0
  const countPeopleWord = `${countUsers} ${getDependOfNumberWord(
    countUsers,
    formatMessage({ id: 'profile.people_words' }).split(' ')
  )}`

  const countProjects = team.countProjects || 0
  const countProjectsWord = `${countProjects} ${getDependOfNumberWord(
    countProjects,
    formatMessage({ id: 'profile.project_words' }).split(' ')
  )}`

  const mockAvatarsTeams = Array.from({ length: countProjects }, () => team.avatarUrl || '')

  const handleClick = () => router.push(`/team/${team.id}`)

  return (
    <Card
      avatarUrl={team.avatarUrl}
      name={team.name}
      countItems={countProjects}
      countItemsWord={countProjectsWord}
      countPeopleWord={countPeopleWord}
      itemsAvatars={mockAvatarsTeams}
      itemsAvatarsShape='circle'
      cardSize={cardSize}
      onClick={handleClick}
      itemsIcon={<Settings2Icon width={14} height={14} color='accentPressed' />}
    />
  )
}
