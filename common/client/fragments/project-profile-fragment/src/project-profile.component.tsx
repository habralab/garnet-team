import React                  from 'react'
import { FC }                 from 'react'
import { FormattedMessage }   from 'react-intl'

import { Project }            from '@shared/data'
import { Condition }          from '@ui/condition'
import { Box }                from '@ui/layout'
import { Column }             from '@ui/layout'
import { Row }                from '@ui/layout'
import { Layout }             from '@ui/layout'
import { Text }               from '@ui/text'
import { Title }              from '@ui/title'
import { WrapperWhite }       from '@ui/wrapper'

import { ButtonEditProject }  from './button-edit-project'
import { ButtonInvite }       from './button-invite'
import { ProfileAvatar }      from './profile-avatar'
import { ProfileDescription } from './profile-description'
import { ProfileTeams }       from './profile-teams'
import { useProjectState }    from './hooks'

export const ProjectProfile: FC = () => {
  const { project, setProject, isMyProject, projectTeams, ownerUser } = useProjectState()

  const handleEditProject = (editedProject: Project) => setProject(editedProject)

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
                <ButtonEditProject project={project} onEditProject={handleEditProject} />
              </Condition>
              <ButtonInvite project={project} />
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
          <ProfileTeams
            ownerUser={ownerUser}
            teams={projectTeams}
            isMyProject={isMyProject}
            projectId={project?.id || ''}
          />
        </WrapperWhite>
      </Condition>
    </Column>
  )
}
