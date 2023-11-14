import React                  from 'react'
import { FC }                 from 'react'
import { FormattedMessage }   from 'react-intl'
import { useState }           from 'react'
import { useIntl }            from 'react-intl'

import { Avatar }             from '@ui/avatar'
import { Input }              from '@ui/input'
import { Label }              from '@ui/input'
import { Textarea }           from '@ui/input'
import { Multiselect }        from '@ui/input'
import { Box }                from '@ui/layout'
import { Column }             from '@ui/layout'
import { Layout }             from '@ui/layout'
import { Row }                from '@ui/layout'
import { Text }               from '@ui/text'

import { ButtonUploadPhoto }  from '../button-upload-photo'
import { FormUserErrorsText } from './form-user.interfaces'
import { FormUserProps }      from './form-user.interfaces'
import { FormUserValues }     from './form-user.interfaces'
import { checkDisabled }      from '../helpers'
import { validateValue }      from '../helpers'
import { defaultErrorsText }  from './form-user.config'
import { formUserErrors }     from './form-user.config'

export const FormUser: FC<FormUserProps> = ({ formValues, handleChange, handleDisabled }) => {
  const [errorsText, setErrorsText] = useState<FormUserErrorsText>(defaultErrorsText)
  const { formatMessage } = useIntl()

  const updateErrorsText = (field: keyof FormUserValues) => () => {
    validateValue(formUserErrors, field, formValues[field], (id, values) =>
      setErrorsText((prev) => {
        const newErrorsText = { ...prev, [field]: id ? formatMessage({ id }, values) : id }

        handleDisabled?.(checkDisabled({ ...formValues }, newErrorsText))

        return newErrorsText
      }))
  }

  const handleChangeAvatar = (url: string) => {
    handleDisabled?.(checkDisabled({ ...formValues, avatar: url }, errorsText))

    handleChange('avatar')(url)
  }

  return (
    <>
      <Row fill>
        <Box height={48} minWidth={125} justifyContent='flex-end' alignItems='center'>
          <Text fontSize='medium' textAlign='right' color='text.gray'>
            <Label htmlFor='name'>
              <FormattedMessage id='shared_ui.form.name' />
            </Label>
          </Text>
        </Box>
        <Layout flexBasis={24} flexShrink={0} />
        <Input
          id='name'
          value={formValues.name}
          onChange={handleChange('name')}
          errorText={errorsText.name}
          onBlur={updateErrorsText('name')}
        />
      </Row>
      <Layout flexBasis={30} flexShrink={0} />
      <Row fill>
        <Box height={48} minWidth={125} justifyContent='flex-end' alignItems='center'>
          <Text fontSize='medium' textAlign='right' color='text.gray'>
            <Label htmlFor='surname'>
              <FormattedMessage id='shared_ui.form.surname' />
            </Label>
          </Text>
        </Box>
        <Layout flexBasis={24} flexShrink={0} />
        <Input
          id='surname'
          value={formValues.surname}
          onChange={handleChange('surname')}
          errorText={errorsText.surname}
          onBlur={updateErrorsText('surname')}
        />
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
          height={176}
          errorText={errorsText.description}
          onBlur={updateErrorsText('description')}
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
          errorText={errorsText.tags}
          onBlur={updateErrorsText('tags')}
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
          <ButtonUploadPhoto onChange={handleChangeAvatar}>
            <Text fontSize='normal' color='currentColor'>
              <FormattedMessage id='shared_ui.form.change' />
            </Text>
          </ButtonUploadPhoto>
          <Layout flexBasis={10} flexShrink={0} />
          <Text fontSize='normal' textAlign='center' color='text.gray'>
            <FormattedMessage id='shared_ui.form.minimum_size' />
          </Text>
        </Column>
      </Row>
    </>
  )
}
