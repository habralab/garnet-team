import React                from 'react'
import { FC }               from 'react'
import { FormattedMessage } from 'react-intl'
import { useState }         from 'react'

import { Button }           from '@ui/button'
import { CardTeam }         from '@ui/card'
import { Condition }        from '@ui/condition'
import { Box }              from '@ui/layout'
import { Grid }             from '@ui/layout'
import { Layout }           from '@ui/layout'
import { Text }             from '@ui/text'

import { ListTeamsProps }   from './list-teams.interfaces'
import { itemPerPage }      from './list-teams.config'

export const ListTeams: FC<ListTeamsProps> = ({ teams }) => {
  const [countPages, setCountPages] = useState(1)

  const handleShowMore = () => setCountPages(countPages + 1)

  return (
    <>
      <Condition match={teams.length === 0}>
        <Text fontSize='semiLarge' color='text.gray' style={{ justifyContent: 'center' }}>
          <FormattedMessage id='user_teams.no_teams' />
        </Text>
      </Condition>
      <Condition match={teams.length > 0}>
        <Grid gap={22} gridWrap='small' justifyContent='space-between'>
          {teams.slice(0, itemPerPage * countPages).map((team) => (
            <CardTeam key={team.id} team={team} cardSize='small' />
          ))}
        </Grid>
        <Condition match={teams.slice(0, itemPerPage * countPages).length !== teams.length}>
          <Layout flexBasis={40} flexShrink={0} />
          <Box justifyContent='center'>
            <Button variant='secondary' size='small' onClick={handleShowMore}>
              <Text fontSize='normal' color='currentColor'>
                <FormattedMessage id='user_teams.show_more' />
              </Text>
            </Button>
          </Box>
        </Condition>
      </Condition>
    </>
  )
}
