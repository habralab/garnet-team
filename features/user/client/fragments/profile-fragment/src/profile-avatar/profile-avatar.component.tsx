import React                  from 'react'
import { FC }                 from 'react'
import { FormattedMessage }   from 'react-intl'
import { useRouter }          from 'next/router'

import { Avatar }             from '@ui/avatar'
import { Button }             from '@ui/button'
import { Condition }          from '@ui/condition'
import { Box }                from '@ui/layout'
import { Column }             from '@ui/layout'
import { Row }                from '@ui/layout'
import { Layout }             from '@ui/layout'
import { NextLink }           from '@ui/link'
import { Text }               from '@ui/text'

import { ButtonCreateTeam }   from './button-create-team'
import { ProfileAvatarProps } from './profile-avatar.interfaces'

export const ProfileAvatar: FC<ProfileAvatarProps> = ({ teams, user, isMyProfile }) => {
  const router = useRouter()

  const handleFindTeams = () => router.push('/teams')

  const handleShowMoreTeams = () => {
    router.push({
      pathname: '/user/teams/[id]',
      query: { id: user?.id },
    })
  }

  return (
    <Column>
      <Avatar size={284} image={user?.avatarUrl} />
      <Layout flexBasis={30} flexShrink={0} />
      <Text fontSize='regular' fontWeight='bold' color='text.secondary'>
        <Condition match={isMyProfile}>
          <FormattedMessage id='profile.my_teams' />
        </Condition>
        <Condition match={!isMyProfile}>
          <FormattedMessage id='profile.teams' />
        </Condition>
      </Text>
      <Layout flexBasis={20} flexShrink={0} />
      <Condition match={teams.length === 0}>
        <Box height={52} justifyContent='center' alignItems='center'>
          <Text fontSize='normal' color='text.gray'>
            <Condition match={isMyProfile}>
              <FormattedMessage id='profile.you_do_not_have_teams' />
            </Condition>
            <Condition match={!isMyProfile}>
              <FormattedMessage id='profile.no_teams' />
            </Condition>
          </Text>
        </Box>
        <Condition match={isMyProfile}>
          <Layout flexBasis={10} flexShrink={0} />
          <Row justifyContent='flex-end' alignItems='center'>
            <Button variant='link' size='micro' onClick={handleFindTeams}>
              <Text fontSize='normal' color='currentColor'>
                <FormattedMessage id='profile.find' />
              </Text>
            </Button>
            <Layout flexBasis={20} flexShrink={0} />
            <ButtonCreateTeam user={user} />
          </Row>
        </Condition>
      </Condition>
      <Condition match={teams.length > 0}>
        <Row justifyContent='space-between'>
          {teams.slice(0, 4).map((team) => (
            <Avatar
              shape='square'
              size={62}
              image={team?.avatarUrl}
              url={`/team/${team?.id}`}
              title={team?.name}
            />
          ))}
        </Row>
      </Condition>
      <Condition match={teams.length > 4}>
        <Layout flexBasis={10} flexShrink={0} />
        <Box justifyContent='flex-end'>
          <NextLink onClick={handleShowMoreTeams}>
            <FormattedMessage id='profile.more_plus' values={{ number: teams.length - 4 }} />
          </NextLink>
        </Box>
      </Condition>
    </Column>
  )
}
