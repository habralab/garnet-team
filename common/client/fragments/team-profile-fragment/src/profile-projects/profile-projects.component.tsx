import React                    from 'react'
import { FC }                   from 'react'
import { FormattedMessage }     from 'react-intl'

import { Button }               from '@ui/button'
import { CardProject }          from '@ui/card'
import { Condition }            from '@ui/condition'
import { Box }                  from '@ui/layout'
import { Grid }                 from '@ui/layout'
import { Column }               from '@ui/layout'
import { Row }                  from '@ui/layout'
import { Layout }               from '@ui/layout'
import { Text }                 from '@ui/text'

import { ButtonCreateProject }  from './button-create-project'
import { ProfileProjectsProps } from './profile-projects.interfaces'

export const ProfileProjects: FC<ProfileProjectsProps> = ({ projects, isMyTeam, ownerUser }) => (
  <Column fill>
    <Row justifyContent='space-between' alignItems='center'>
      <Text fontSize='regular' fontWeight='bold' color='text.secondary'>
        <FormattedMessage id='profile.projects' />
        <Condition match={projects.length > 0}>{`: ${projects.length}`}</Condition>
      </Text>
      <Condition match={isMyTeam && projects.length > 0}>
        <Box alignItems='center'>
          <Button variant='link' size='micro'>
            <Text fontSize='normal' color='currentColor'>
              <FormattedMessage id='profile.manage' />
            </Text>
          </Button>
          <Layout width={64} />
          <ButtonCreateProject user={ownerUser} />
        </Box>
      </Condition>
    </Row>
    <Layout flexBasis={30} flexShrink={0} />
    <Condition match={projects.length > 0}>
      <Grid gap={22} gridWrap='normal' justifyContent='space-between'>
        {projects.map((project) => (
          <CardProject key={project.id} project={project} cardSize='normal' />
        ))}
      </Grid>
    </Condition>
    <Condition match={projects.length === 0}>
      <Column justifyContent='center' alignItems='center' height={284}>
        <Text
          fontSize='semiLarge'
          fontWeight='semiBold'
          color='text.gray'
          textAlign='center'
          opacity={0.6}
        >
          <FormattedMessage id='profile.no_projects' />
        </Text>
        <Condition match={isMyTeam}>
          <Layout flexBasis={30} flexShrink={0} />
          <ButtonCreateProject user={ownerUser} />
          <Layout flexBasis={20} flexShrink={0} />
          <Button variant='link' size='micro'>
            <Text fontSize='normal' color='currentColor'>
              <FormattedMessage id='profile.manage' />
            </Text>
          </Button>
        </Condition>
      </Column>
    </Condition>
  </Column>
)
