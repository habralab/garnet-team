import React                from 'react'
import { FC }               from 'react'
import { FormattedMessage } from 'react-intl'
import { useIntl }          from 'react-intl'

import { Avatar }           from '@ui/avatar'
import { Button }           from '@ui/button'
import { Condition }        from '@ui/condition'
import { Input }            from '@ui/input'
import { Label }            from '@ui/input'
import { Textarea }         from '@ui/input'
import { Box }              from '@ui/layout'
import { Column }           from '@ui/layout'
import { Layout }           from '@ui/layout'
import { Row }              from '@ui/layout'
import { Text }             from '@ui/text'

import { FormTeamProps }    from './form-team.interfaces'

export const FormTeam: FC<FormTeamProps> = ({ formValues, handleChange }) => {
  const { formatMessage } = useIntl()

  return (
    <>
      <Row fill alignItems='center'>
        <Box minWidth={125} justifyContent='flex-end'>
          <Text fontSize='medium' textAlign='right' color='text.gray'>
            <Label htmlFor='name'>
              <FormattedMessage id='profile.naming' />
            </Label>
          </Text>
        </Box>
        <Layout flexBasis={24} flexShrink={0} />
        <Input
          id='name'
          value={formValues.name}
          placeholder={formatMessage({ id: 'profile.my_team' })}
          onChange={handleChange('name')}
        />
      </Row>
      <Layout flexBasis={30} flexShrink={0} />
      <Row fill alignItems='flex-start'>
        <Box height={50} minWidth={125} justifyContent='flex-end' alignItems='center'>
          <Text fontSize='medium' textAlign='right' color='text.gray'>
            <Label htmlFor='description'>
              <FormattedMessage id='profile.description' />
            </Label>
          </Text>
        </Box>
        <Layout flexBasis={24} flexShrink={0} />
        <Textarea
          id='description'
          value={formValues.description}
          placeholder={formatMessage({ id: 'profile.description_team' })}
          onChange={handleChange('description')}
          style={{ height: 176, resize: 'none' }}
          min={150}
          max={1000}
        />
      </Row>
      <Layout flexBasis={10} flexShrink={0} />
      <Row fill justifyContent='flex-end'>
        <Text fontSize='normal' color='text.gray'>
          <FormattedMessage id='profile.from_to_symbols' />
        </Text>
      </Row>
      <Layout flexBasis={30} flexShrink={0} />
      <Row fill alignItems='flex-start'>
        <Box height={50} minWidth={125} justifyContent='flex-end' alignItems='center'>
          <Text fontSize='medium' textAlign='right' color='text.gray'>
            <Label htmlFor='tags'>
              <FormattedMessage id='profile.skills' />
            </Label>
          </Text>
        </Box>
        <Layout flexBasis={24} flexShrink={0} />
        <Input
          id='tags'
          value={formValues.tags}
          placeholder={formatMessage({ id: 'profile.choose_skill' })}
          onChange={handleChange('tags')}
        />
      </Row>
      <Layout flexBasis={30} flexShrink={0} />
      <Row fill alignItems='flex-start'>
        <Box height={50} minWidth={125} justifyContent='flex-end' alignItems='center'>
          <Text fontSize='medium' textAlign='right' color='text.gray'>
            <FormattedMessage id='profile.image' />
          </Text>
        </Box>
        <Layout flexBasis={24} flexShrink={0} />
        <Column fill maxWidth={170} alignItems='center'>
          <Condition match={Boolean(formValues?.avatar)}>
            <Avatar image={formValues?.avatar} size={150} />
            <Layout flexBasis={10} flexShrink={0} />
            <Button variant='link' size='micro'>
              <Text fontSize='normal' color='currentColor'>
                <FormattedMessage id='profile.change' />
              </Text>
            </Button>
          </Condition>
          <Condition match={!formValues?.avatar}>
            <Box height={50} justifyContent='center' alignItems='center'>
              <Button variant='link' size='micro'>
                <Text fontSize='medium' color='currentColor'>
                  <FormattedMessage id='profile.load' />
                </Text>
              </Button>
            </Box>
          </Condition>
          <Layout flexBasis={10} flexShrink={0} />
          <Text fontSize='normal' textAlign='center' color='text.gray'>
            <FormattedMessage id='profile.minimum_size' />
          </Text>
        </Column>
      </Row>
    </>
  )
}
