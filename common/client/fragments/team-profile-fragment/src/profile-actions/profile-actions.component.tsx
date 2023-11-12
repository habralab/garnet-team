import React                   from 'react'
import { FC }                  from 'react'
import { FormattedMessage }    from 'react-intl'

import { Button }              from '@ui/button'
import { Condition }           from '@ui/condition'
import { Layout }              from '@ui/layout'
import { NextLink }            from '@ui/link'
import { Text }                from '@ui/text'

import { ButtonEditTeam }      from './button-edit-team'
import { ButtonJoinRequest }   from './button-join-request'
import { ProfileActionsProps } from './profile-actions.interfaces'

export const ProfileActions: FC<ProfileActionsProps> = ({
  team,
  onEditTeam,
  joinRequest,
  isMyTeam = false,
}) => (
  <>
    <Condition match={isMyTeam}>
      <ButtonEditTeam team={team} onEditTeam={onEditTeam} />
      <Layout width={62} />
      <NextLink path='/team/invite' href='/team/invite'>
        <Button variant='primary' size='normal'>
          <Text fontSize='medium' color='currentColor'>
            <FormattedMessage id='profile.invite_to_team' />
          </Text>
        </Button>
      </NextLink>
    </Condition>
    <Condition match={!isMyTeam}>
      <ButtonJoinRequest team={team} joinRequest={joinRequest} />
    </Condition>
  </>
)
