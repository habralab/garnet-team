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

export const ProfileDescription: FC<ProfileDescriptionProps> = ({ team }) => (
  <Row fill flexWrap='wrap' gap={32} justifyContent='space-between'>
    <Column height='max-content' minWidth={300} maxWidth='100%' flex='1 0 47%'>
      <Text fontSize='preLarge' fontWeight='bold' color='text.secondary'>
        {team?.name}
      </Text>
      <Layout flexBasis={30} flexShrink={0} />
      <Text fontSize='semiMedium' color='text.gray'>
        <FormattedMessage id='profile.about_team' />
      </Text>
      <Layout flexBasis={10} flexShrink={0} />
      <Text fontSize='medium' color='text.secondary' wordBreak='break-word'>
        {team?.description}
      </Text>
    </Column>
    <Column height='max-content' minWidth={300} maxWidth='100%' flex='1 0 47%'>
      <Layout flexBasis={[0, 0, 0, 60]} />
      <Text fontSize='semiMedium' color='text.gray'>
        <FormattedMessage id='profile.required_skills' />
      </Text>
      <Layout flexBasis={10} flexShrink={0} />
      <Row flexWrap='wrap' maxHeight={72} overflow='hidden' gap={10}>
        {team?.tags?.map((tag) => (
          <Tag key={tag}>{tag}</Tag>
        ))}
      </Row>
      <Layout flexBasis={10} flexShrink={0} />
      <Box justifyContent='flex-end'>
        <ButtonWatchAllSkills skills={team?.tags} />
      </Box>
      <Layout flexBasis={70} flexShrink={0} />
    </Column>
  </Row>
)
