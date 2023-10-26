import React                       from 'react'
import { FC }                      from 'react'
import { FormattedMessage }        from 'react-intl'

import { Box }                     from '@ui/layout'
import { Column }                  from '@ui/layout'
import { Row }                     from '@ui/layout'
import { Layout }                  from '@ui/layout'
import { Tag }                     from '@ui/tag'
import { Text }                    from '@ui/text'

import { ButtonWatchAllSkills }    from './button-watch-all-skills'
import { ProfileDescriptionProps } from './profile-description.interfaces'

export const ProfileDescription: FC<ProfileDescriptionProps> = ({ user }) => (
  <Column fill maxWidth={600}>
    <Text fontSize='preLarge' fontWeight='bold' color='text.secondary'>
      {user?.userName}
    </Text>
    <Layout flexBasis={10} flexShrink={0} />
    <Text fontSize='semiMedium' color='text.gray'>
      <FormattedMessage id='profile.about' />
    </Text>
    <Layout flexBasis={10} flexShrink={0} />
    <Text fontSize='medium' color='text.secondary'>
      {user?.description}
    </Text>
    <Layout flexBasis={20} flexShrink={0} />
    <Text fontSize='semiMedium' color='text.gray'>
      <FormattedMessage id='profile.skills' />
    </Text>
    <Layout flexBasis={10} flexShrink={0} />
    <Row flexWrap='wrap' maxHeight={72} overflow='hidden' style={{ gap: 10 }}>
      {user?.tags?.map((tag) => (
        <Tag key={tag}>{tag}</Tag>
      ))}
    </Row>
    <Layout flexBasis={10} flexShrink={0} />
    <Box justifyContent='flex-end'>
      <ButtonWatchAllSkills skills={user?.tags} />
    </Box>
  </Column>
)
