import React                from 'react'
import { FC }               from 'react'
import { FormattedMessage } from 'react-intl'
import { useIntl }          from 'react-intl'

import { Avatar }           from '@ui/avatar'
import { Button }           from '@ui/button'
import { Input }            from '@ui/input'
import { Label }            from '@ui/input'
import { Textarea }         from '@ui/input'
import { Multiselect }      from '@ui/input'
import { Box }              from '@ui/layout'
import { Column }           from '@ui/layout'
import { Layout }           from '@ui/layout'
import { Row }              from '@ui/layout'
import { Text }             from '@ui/text'

import { FormUserProps }    from './form-user.interfaces'

export const FormUser: FC<FormUserProps> = ({ formValues, handleChange }) => {
  const { formatMessage } = useIntl()

  return (
    <>
      <Row fill alignItems='center'>
        <Box minWidth={125} justifyContent='flex-end'>
          <Text fontSize='medium' textAlign='right' color='text.gray'>
            <Label htmlFor='name'>
              <FormattedMessage id='shared_ui.form.name' />
            </Label>
          </Text>
        </Box>
        <Layout flexBasis={24} flexShrink={0} />
        <Input id='name' value={formValues.name} onChange={handleChange('name')} />
      </Row>
      <Layout flexBasis={30} flexShrink={0} />
      <Row fill alignItems='center'>
        <Box minWidth={125} justifyContent='flex-end'>
          <Text fontSize='medium' textAlign='right' color='text.gray'>
            <Label htmlFor='surname'>
              <FormattedMessage id='shared_ui.form.surname' />
            </Label>
          </Text>
        </Box>
        <Layout flexBasis={24} flexShrink={0} />
        <Input id='surname' value={formValues.surname} onChange={handleChange('surname')} />
      </Row>
      <Layout flexBasis={30} flexShrink={0} />
      <Row fill alignItems='flex-start'>
        <Box height={50} minWidth={125} justifyContent='flex-end' alignItems='center'>
          <Text fontSize='medium' textAlign='right' color='text.gray'>
            <Label htmlFor='description'>
              <FormattedMessage id='shared_ui.form.about' />
            </Label>
          </Text>
        </Box>
        <Layout flexBasis={24} flexShrink={0} />
        <Textarea
          id='description'
          value={formValues.description}
          onChange={handleChange('description')}
          style={{ height: 176, resize: 'none' }}
          min={150}
          max={1000}
        />
      </Row>
      <Layout flexBasis={10} flexShrink={0} />
      <Row fill justifyContent='flex-end'>
        <Text fontSize='normal' color='text.gray'>
          <FormattedMessage id='shared_ui.form.from_to_symbols' />
        </Text>
      </Row>
      <Layout flexBasis={30} flexShrink={0} />
      <Row fill alignItems='flex-start'>
        <Box height={50} minWidth={125} justifyContent='flex-end' alignItems='center'>
          <Text fontSize='medium' textAlign='right' color='text.gray'>
            <Label htmlFor='tags'>
              <FormattedMessage id='shared_ui.form.skills' />
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
          <Avatar image={formValues?.avatar} size={150} />
          <Layout flexBasis={10} flexShrink={0} />
          <Button variant='link' size='micro'>
            <Text fontSize='normal' color='currentColor'>
              <FormattedMessage id='shared_ui.form.change' />
            </Text>
          </Button>
          <Layout flexBasis={10} flexShrink={0} />
          <Text fontSize='normal' textAlign='center' color='text.gray'>
            <FormattedMessage id='shared_ui.form.minimum_size' />
          </Text>
        </Column>
      </Row>
    </>
  )
}
