import React                    from 'react'
import { FC }                   from 'react'
import { FormattedMessage }     from 'react-intl'
import { useRouter }            from 'next/router'

import { Button }               from '@ui/button'
import { CardProject }          from '@ui/card'
import { Condition }            from '@ui/condition'
import { Box }                  from '@ui/layout'
import { Column }               from '@ui/layout'
import { Row }                  from '@ui/layout'
import { Layout }               from '@ui/layout'
import { Text }                 from '@ui/text'

import { ButtonCreateProject }  from './button-create-project'
import { ProfileProjectsProps } from './profile-projects.interfaces'

export const ProfileProjects: FC<ProfileProjectsProps> = ({ projects, isMyProfile, user }) => {
  const router = useRouter()

  const handleFindProjects = () => router.push('/projects')

  return (
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
            <Button variant='link' size='micro'>
              <Text fontSize='normal' color='currentColor'>
                <FormattedMessage id='profile.manage' />
              </Text>
            </Button>
            <Layout width={64} />
            <ButtonCreateProject user={user} />
          </Box>
        </Condition>
      </Row>
      <Layout flexBasis={30} flexShrink={0} />
      <Condition match={projects.length > 0}>
        <Row
          display='grid'
          style={{ gap: 32, gridTemplateColumns: `repeat(auto-fill, 284px)` }}
          justifyContent='space-between'
        >
          {projects.map((project) => (
            <CardProject key={project.id} project={project} cardSize='large' />
          ))}
        </Row>
      </Condition>
      <Condition match={projects.length === 0}>
        <Column justifyContent='center' alignItems='center' height={284}>
          <Text fontSize='semiLarge' fontWeight='semiBold' color='text.gray' textAlign='center'>
            <FormattedMessage id='profile.no_projects' />
          </Text>
          <Condition match={isMyProfile}>
            <Layout flexBasis={30} flexShrink={0} />
            <Button variant='secondary' size='small'>
              <Text fontSize='normal' color='currentColor'>
                <FormattedMessage id='profile.create_project' />
              </Text>
            </Button>
            <Layout flexBasis={20} flexShrink={0} />
            <Button variant='link' size='micro' onClick={handleFindProjects}>
              <Text fontSize='normal' color='currentColor'>
                <FormattedMessage id='profile.find_project' />
              </Text>
            </Button>
          </Condition>
        </Column>
      </Condition>
    </Column>
  )
}
