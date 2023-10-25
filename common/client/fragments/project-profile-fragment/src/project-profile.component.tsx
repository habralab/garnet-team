import React                  from 'react'
import { FC }                 from 'react'
import { FormattedMessage }   from 'react-intl'
import { useRouter }          from 'next/router'
import { useEffect }          from 'react'
import { useState }           from 'react'

import { Condition }          from '@ui/condition'
import { Box }                from '@ui/layout'
import { Column }             from '@ui/layout'
import { Row }                from '@ui/layout'
import { Layout }             from '@ui/layout'
import { Text }               from '@ui/text'
import { Title }              from '@ui/title'
import { WrapperWhite }       from '@ui/wrapper'
import { mockAuthUserId }     from '@shared/data'
import { getMockProject }     from '@shared/data'
import { getMockUser }        from '@shared/data'

import { ButtonEditProject }  from './button-edit-project'
import { ProfileAvatar }      from './profile-avatar'
import { ProfileDescription } from './profile-description'
import { ProfileTeams }       from './profile-teams'

export const ProjectProfile: FC = () => {
  const router = useRouter()
  const [isMyProject, setIsMyProject] = useState(false)

  const { project, projectTeams } = getMockProject(String(router.query.id) || '')
  const ownerUser = getMockUser(project?.ownerUserId || '')?.user

  useEffect(() => {
    if (project?.ownerUserId === mockAuthUserId) setIsMyProject(true)
  }, [project])

  return (
    <Column fill>
      <Title hasBack>
        <Condition match={isMyProject && Boolean(project)}>
          <FormattedMessage id='profile.my_project' />
        </Condition>
        <Condition match={!isMyProject || !project}>
          <FormattedMessage id='profile.project' />
        </Condition>
      </Title>
      <WrapperWhite>
        <Row>
          <Condition match={Boolean(project)}>
            <ProfileAvatar project={project} ownerUser={ownerUser} />
            <Layout flexBasis={32} />
            <ProfileDescription project={project} />
            <Box position='absolute' bottom={32} right={32}>
              <Condition match={isMyProject}>
                <ButtonEditProject project={project} />
              </Condition>
            </Box>
          </Condition>
          <Condition match={!project}>
            <Column fill alignItems='center'>
              <Layout flexBasis={50} />
              <Text fontSize='semiLarge' fontWeight='semiBold' color='text.gray' textAlign='center'>
                <FormattedMessage id='profile.project_not_found' />
              </Text>
              <Layout flexBasis={50} />
            </Column>
          </Condition>
        </Row>
      </WrapperWhite>
      <Condition match={Boolean(project)}>
        <Layout flexBasis={32} flexShrink={0} />
        <WrapperWhite>
          <Column>
            <ProfileTeams ownerUser={ownerUser} teams={projectTeams} isMyProject={isMyProject} />
          </Column>
        </WrapperWhite>
      </Condition>
    </Column>
  )
}
