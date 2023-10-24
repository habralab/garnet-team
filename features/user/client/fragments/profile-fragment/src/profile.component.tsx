import React                  from 'react'
import { FC }                 from 'react'
import { FormattedMessage }   from 'react-intl'
import { useRouter }          from 'next/router'
import { useEffect }          from 'react'
import { useState }           from 'react'

import { Button }             from '@ui/button'
import { Condition }          from '@ui/condition'
import { Box }                from '@ui/layout'
import { Column }             from '@ui/layout'
import { Row }                from '@ui/layout'
import { Layout }             from '@ui/layout'
import { Text }               from '@ui/text'
import { Title }              from '@ui/title'
import { WrapperWhite }       from '@ui/wrapper'
import { mockAuthUserId }     from '@shared/data'
import { getMockUser }        from '@shared/data'

import { ButtonEditProfile }  from './button-edit-profile'
import { ProfileAvatar }      from './profile-avatar'
import { ProfileDescription } from './profile-description'
import { ProfileProjects }    from './profile-projects'

export const Profile: FC = () => {
  const router = useRouter()
  const [isMyProfile, setIsMyProfile] = useState(false)

  const userData = getMockUser(String(router.query.id) || '')

  const user = userData?.user
  const teams = userData?.teams || []
  const projects = userData?.projects || []

  useEffect(() => {
    if (user?.id === mockAuthUserId) setIsMyProfile(true)
  }, [user])

  return (
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
                <ButtonEditProfile user={user} />
              </Condition>
              <Condition match={!isMyProfile}>
                <Button variant='primary' size='normal'>
                  <Text fontSize='medium' color='currentColor'>
                    <FormattedMessage id='profile.invite' />
                  </Text>
                </Button>
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
}
