import React                  from 'react'
import { FC }                 from 'react'
import { FormattedMessage }   from 'react-intl'

import { Avatar }             from '@ui/avatar'
import { Column }             from '@ui/layout'
import { Row }                from '@ui/layout'
import { Layout }             from '@ui/layout'
import { Text }               from '@ui/text'

import { ProfileAvatarProps } from './profile-avatar.interfaces'

export const ProfileAvatar: FC<ProfileAvatarProps> = ({ project, ownerUser }) => (
  <Column>
    <Avatar size={180} shape='square' image={project?.avatarUrl} />
    <Layout flexBasis={20} flexShrink={0} />
    <Row>
      <Avatar size={50} image={ownerUser?.avatarUrl} url={`/user/${ownerUser?.id}`} />
      <Layout flexBasis={10} flexShrink={0} />
      <Column fill justifyContent='center'>
        <Text fontSize='normal' color='text.gray'>
          <FormattedMessage id='profile.project_owner' />
        </Text>
        <Layout flexBasis={8} flexShrink={0} />
        <Text fontSize='normal' color='text.secondary'>
          {ownerUser?.userName}
        </Text>
      </Column>
    </Row>
  </Column>
)
