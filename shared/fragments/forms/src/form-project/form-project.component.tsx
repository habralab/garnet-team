import React                from 'react'
import { FC }               from 'react'
import { FormattedMessage } from 'react-intl'
import { useIntl }          from 'react-intl'

import { Avatar }           from '@ui/avatar'
import { Button }           from '@ui/button'
import { Condition }        from '@ui/condition'
import { Input }            from '@ui/input'
import { Multiselect }      from '@ui/input'
import { Label }            from '@ui/input'
import { Textarea }         from '@ui/input'
import { Box }              from '@ui/layout'
import { Column }           from '@ui/layout'
import { Layout }           from '@ui/layout'
import { Row }              from '@ui/layout'
import { Text }             from '@ui/text'

import { FormProjectProps } from './form-project.interfaces'

export const FormProject: FC<FormProjectProps> = ({ formValues, handleChange }) => {
  const { formatMessage } = useIntl()

  return (
    <>
      <Row fill alignItems='center'>
        <Box minWidth={125} justifyContent='flex-end'>
          <Text fontSize='medium' textAlign='right' color='text.gray'>
            <Label htmlFor='name'>
              <FormattedMessage id='shared_ui.form.naming' />
            </Label>
          </Text>
        </Box>
        <Layout flexBasis={24} flexShrink={0} />
        <Input
          id='name'
          value={formValues.name}
          placeholder={formatMessage({ id: 'shared_ui.form.my_project' })}
          onChange={handleChange('name')}
        />
      </Row>
      <Layout flexBasis={30} flexShrink={0} />
      <Row fill alignItems='flex-start'>
        <Box height={50} minWidth={125} justifyContent='flex-end' alignItems='center'>
          <Text fontSize='medium' textAlign='right' color='text.gray'>
            <Label htmlFor='description'>
              <FormattedMessage id='shared_ui.form.description' />
            </Label>
          </Text>
        </Box>
        <Layout flexBasis={24} flexShrink={0} />
        <Textarea
          id='description'
          value={formValues.description}
          placeholder={formatMessage({ id: 'shared_ui.form.description_project' })}
          onChange={handleChange('description')}
          style={{ height: 176, resize: 'none' }}
          min={150}
          max={1000}
        />
      </Row>
      <Layout flexBasis={10} flexShrink={0} />
      <Row fill justifyContent='flex-end'>
        <Text fontSize='normal' textAlign='right' color='text.gray'>
          <FormattedMessage id='shared_ui.form.from_to_symbols' />
        </Text>
      </Row>
      <Layout flexBasis={30} flexShrink={0} />
      <Row fill alignItems='flex-start'>
        <Box height={50} minWidth={125} justifyContent='flex-end' alignItems='center'>
          <Text fontSize='medium' textAlign='right' color='text.gray'>
            <Label htmlFor='tags'>
              <FormattedMessage id='shared_ui.form.required_skills' />
            </Label>
          </Text>
        </Box>
        <Layout flexBasis={24} flexShrink={0} />
        <Multiselect
          id='tags'
          placeholder={formatMessage({ id: 'shared_ui.form.enter_skill' })}
          value={formValues.tags}
          onChange={handleChange('tags')}
        />
      </Row>
      <Layout flexBasis={30} flexShrink={0} />
      <Row fill alignItems='flex-start'>
        <Box height={50} minWidth={125} justifyContent='flex-end' alignItems='center'>
          <Text fontSize='medium' textAlign='right' color='text.gray'>
            <FormattedMessage id='shared_ui.form.image' />
          </Text>
        </Box>
        <Layout flexBasis={24} flexShrink={0} />
        <Column fill maxWidth={170} alignItems='center'>
          <Condition match={Boolean(formValues?.avatar)}>
            <Avatar image={formValues?.avatar} shape='square' size={150} />
            <Layout flexBasis={10} flexShrink={0} />
            <Button variant='link' size='micro'>
              <Text fontSize='normal' color='currentColor'>
                <FormattedMessage id='shared_ui.form.change' />
              </Text>
            </Button>
          </Condition>
          <Condition match={!formValues?.avatar}>
            <Box height={50} justifyContent='center' alignItems='center'>
              <Button variant='link' size='micro'>
                <Text fontSize='medium' color='currentColor'>
                  <FormattedMessage id='shared_ui.form.load' />
                </Text>
              </Button>
            </Box>
          </Condition>
          <Layout flexBasis={10} flexShrink={0} />
          <Text fontSize='normal' textAlign='center' color='text.gray'>
            <FormattedMessage id='shared_ui.form.minimum_size' />
          </Text>
        </Column>
      </Row>
    </>
  )
}
