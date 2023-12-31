import React                  from 'react'
import { FC }                 from 'react'
import { FormattedMessage }   from 'react-intl'

import { Condition }          from '@ui/condition'
import { Box }                from '@ui/layout'
import { Column }             from '@ui/layout'
import { Row }                from '@ui/layout'
import { Layout }             from '@ui/layout'
import { Text }               from '@ui/text'
import { Title }              from '@ui/title'
import { WrapperWhite }       from '@ui/wrapper'

import { ButtonEditProfile }  from './button-edit-profile'
import { ButtonInvite }       from './button-invite'
import { ProfileAvatar }      from './profile-avatar'
import { ProfileDescription } from './profile-description'
import { ProfileProjects }    from './profile-projects'
import { UserProfileProps }   from './user-profile.interfaces'

export const UserProfile: FC<UserProfileProps> = ({
  user,
  isMyProfile,
  onEditUser,
  projects,
  teams,
}) => (
  <Column fill>
    <Condition match={!isMyProfile || !user}>
      <Title>
        <FormattedMessage id='profile.user_profile' />
      </Title>
    </Condition>
    <Condition match={isMyProfile && Boolean(user)}>
      <Title>
        <FormattedMessage id='profile.my_profile' />
      </Title>
    </Condition>
    <WrapperWhite>
      <Row>
        <Condition match={Boolean(user)}>
          <ProfileAvatar user={user} isMyProfile={isMyProfile} teams={teams} />
          <Layout flexBasis={32} />
          <ProfileDescription user={user} />
          <Box position='absolute' bottom={32} right={32}>
            <Condition match={isMyProfile}>
              <ButtonEditProfile user={user} onEditUser={onEditUser} />
            </Condition>
            <Condition match={!isMyProfile}>
              <ButtonInvite user={user} />
            </Condition>
          </Box>
        </Condition>
        <Condition match={!user}>
          <Column fill alignItems='center'>
            <Layout flexBasis={50} />
            <Text fontSize='semiLarge' fontWeight='semiBold' color='text.gray' textAlign='center'>
              <FormattedMessage id='profile.profile_not_found' />
            </Text>
            <Layout flexBasis={50} />
          </Column>
        </Condition>
      </Row>
    </WrapperWhite>
    <Condition match={Boolean(user)}>
      <Layout flexBasis={32} flexShrink={0} />
      <WrapperWhite>
        <Column>
          <ProfileProjects user={user} projects={projects} isMyProfile={isMyProfile} />
        </Column>
      </WrapperWhite>
    </Condition>
  </Column>
)
