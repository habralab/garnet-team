import React                    from 'react'
import { FC }                   from 'react'
import { FormattedMessage }     from 'react-intl'

import { Button }               from '@ui/button'
import { CardProject }          from '@ui/card'
import { Condition }            from '@ui/condition'
import { Box }                  from '@ui/layout'
import { Column }               from '@ui/layout'
import { Row }                  from '@ui/layout'
import { Grid }                 from '@ui/layout'
import { Layout }               from '@ui/layout'
import { NextLink }             from '@ui/link'
import { Text }                 from '@ui/text'
import { routes }               from '@shared/routes'

import { ButtonCreateProject }  from './button-create-project'
import { ProfileProjectsProps } from './profile-projects.interfaces'

export const ProfileProjects: FC<ProfileProjectsProps> = ({ projects, isMyProfile, user }) => (
  <Column fill>
    <Row justifyContent='space-between' alignItems='center'>
      <Text fontSize='regular' fontWeight='bold' color='text.secondary'>
        <Condition match={isMyProfile}>
          <FormattedMessage id='profile.my_projects' />
        </Condition>
        <Condition match={!isMyProfile}>
          <FormattedMessage id='profile.projects' />
        </Condition>
        <Condition match={projects.length > 0}>{`: ${projects.length}`}</Condition>
      </Text>
      <Condition match={isMyProfile && projects.length > 0}>
        <Box alignItems='center'>
          <NextLink path={routes.usersProjects(user?.id || '')}>
            <Button variant='link' size='micro'>
              <Text fontSize='normal' color='currentColor'>
                <FormattedMessage id='profile.manage' />
              </Text>
            </Button>
          </NextLink>
          <Layout width={64} />
          <ButtonCreateProject user={user} withIcon />
        </Box>
      </Condition>
    </Row>
    <Layout flexBasis={30} flexShrink={0} />
    <Condition match={projects.length > 0}>
      <Grid justifyContent='space-between' gap={32} gridWrap='large'>
        {projects.map((project) => (
          <CardProject key={project.id} project={project} cardSize='large' />
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
        <Condition match={isMyProfile}>
          <Layout flexBasis={30} flexShrink={0} />
          <ButtonCreateProject user={user} />
          <Layout flexBasis={20} flexShrink={0} />
          <NextLink path={routes.projects}>
            <Button variant='link' size='micro'>
              <Text fontSize='normal' color='currentColor'>
                <FormattedMessage id='profile.find_project' />
              </Text>
            </Button>
          </NextLink>
        </Condition>
      </Column>
    </Condition>
  </Column>
)
