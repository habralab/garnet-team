import React                  from 'react'
import { FC }                 from 'react'
import { FormattedMessage }   from 'react-intl'
import { useRouter }          from 'next/router'
import { useEffect }          from 'react'
import { useState }           from 'react'

import { User }               from '@shared/data'
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
import { useGetUser }         from '@shared/data'

import { ButtonEditProfile }  from './button-edit-profile'
import { ProfileAvatar }      from './profile-avatar'
import { ProfileDescription } from './profile-description'
import { ProfileProjects }    from './profile-projects'

export const Profile: FC = () => {
  const router = useRouter()
  const queryId = typeof router.query.id === 'string' ? router.query.id : ''

  const [user, setUser] = useState<User>()
  const [isMyProfile, setIsMyProfile] = useState(false)

  const { user: fetchedUser, teams, projects } = useGetUser({ id: queryId, skip: 0, take: 20 })

  useEffect(() => {
    if (fetchedUser) {
      const { avatarUrl, description, id, tags } = fetchedUser

      if (!avatarUrl || !description || !tags || tags.length === 0) {
        router.push('/onboarding')
      }

      if (id === mockAuthUserId) setIsMyProfile(true)

      setUser(fetchedUser)
    }
  }, [router, fetchedUser])

  const handleEditUser = (editedUser: User) => setUser(editedUser)

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
                <ButtonEditProfile user={user} onEditUser={handleEditUser} />
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
