import React                   from 'react'
import { FC }                  from 'react'
import { FormattedMessage }    from 'react-intl'

import { Background }          from '@ui/background'
import { Condition }           from '@ui/condition'
import { ClearIcon }           from '@ui/icon'
import { Box }                 from '@ui/layout'
import { Layout }              from '@ui/layout'
import { Row }                 from '@ui/layout'
import { Tag }                 from '@ui/tag'
import { Text }                from '@ui/text'

import { DropdownSkillsProps } from './dropdown-skills.interfaces'

export const DropdownSkills: FC<DropdownSkillsProps> = ({
  options = [],
  onClick,
  onChangeOption,
}) => (
  <Background
    fill
    color='white'
    borderRadius='medium'
    border='lightGrayForty'
    padding='16px'
    flexDirection='column'
    position='relative'
  >
    <Box position='absolute' top={16} right={16} style={{ cursor: 'pointer' }} onClick={onClick}>
      <ClearIcon width={24} height={24} color='gray' />
    </Box>
    <Condition match={options.length === 0}>
      <Layout flexBasis={30} flexShrink={0} />
      <Text fontSize='semiLarge' color='text.gray' style={{ justifyContent: 'center' }}>
        <FormattedMessage id='shared_ui.skills_not_found' />
      </Text>
      <Layout flexBasis={30} flexShrink={0} />
    </Condition>
    <Condition match={options.length > 0}>
      <Text fontSize='semiMedium' color='text.secondary'>
        <FormattedMessage id='shared_ui.choose_skill' />:
      </Text>
      <Layout flexBasis={12} flexShrink={0} />
      <Row flexWrap='wrap' flex='auto' style={{ gap: 10 }}>
        {options.map((item) => (
          <Tag key={item} onClick={() => onChangeOption?.(item)}>
            {item}
          </Tag>
        ))}
      </Row>
    </Condition>
  </Background>
)
