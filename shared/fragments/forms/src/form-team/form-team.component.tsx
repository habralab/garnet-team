import React                  from 'react'
import { FC }                 from 'react'
import { FormattedMessage }   from 'react-intl'
import { useState }           from 'react'
import { useIntl }            from 'react-intl'

import { Avatar }             from '@ui/avatar'
import { Condition }          from '@ui/condition'
import { Input }              from '@ui/input'
import { Multiselect }        from '@ui/input'
import { Label }              from '@ui/input'
import { Textarea }           from '@ui/input'
import { Box }                from '@ui/layout'
import { Column }             from '@ui/layout'
import { Layout }             from '@ui/layout'
import { Row }                from '@ui/layout'
import { Text }               from '@ui/text'

import { ButtonUploadPhoto }  from '../button-upload-photo'
import { CallbackValues }     from '../helpers'
import { FormTeamErrorsText } from './form-team.interfaces'
import { FormTeamProps }      from './form-team.interfaces'
import { FormTeamValues }     from './form-team.interfaces'
import { checkDisabled }      from '../helpers'
import { validateValue }      from '../helpers'
import { defaultErrorsText }  from './form-team.config'
import { formTeamErrors }     from './form-team.config'

export const FormTeam: FC<FormTeamProps> = ({ formValues, handleChange, handleDisabled }) => {
  const [errorsText, setErrorsText] = useState<FormTeamErrorsText>(defaultErrorsText)
  const { formatMessage } = useIntl()

  const handleChangeErrors = (field: keyof FormTeamValues, value?: string | string[]) =>
    (id: string, values?: CallbackValues) => {
      setErrorsText((prev) => {
        const newErrorsText = { ...prev, [field]: id ? formatMessage({ id }, values) : id }
        const newFormValues = { ...formValues, [field]: value ?? formValues[field] }

        handleDisabled?.(checkDisabled(newFormValues, newErrorsText))

        return newErrorsText
      })
    }

  const updateErrorsText = (field: keyof FormTeamValues) => () => {
    validateValue(formTeamErrors, field, formValues[field], handleChangeErrors(field))
  }

  const handleChangeTags = (tags: string[] | string) => {
    validateValue(formTeamErrors, 'tags', tags, handleChangeErrors('tags', tags))

    handleChange('tags')(tags)
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
              <FormattedMessage id='shared_ui.form.naming' />
            </Label>
          </Text>
        </Box>
        <Layout flexBasis={24} flexShrink={0} />
        <Input
          id='name'
          value={formValues.name}
          placeholder={formatMessage({ id: 'shared_ui.form.my_team' })}
          onChange={handleChange('name')}
          errorText={errorsText.name}
          onBlur={updateErrorsText('name')}
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
          placeholder={formatMessage({ id: 'shared_ui.form.description_team' })}
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
          onChange={handleChangeTags}
          errorText={errorsText.tags}
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
            <ButtonUploadPhoto onChange={handleChangeAvatar} shape='square'>
              <Text fontSize='normal' color='currentColor'>
                <FormattedMessage id='shared_ui.form.change' />
              </Text>
            </ButtonUploadPhoto>
          </Condition>
          <Condition match={!formValues?.avatar}>
            <Box height={50} justifyContent='center' alignItems='center'>
              <ButtonUploadPhoto onChange={handleChangeAvatar} shape='square'>
                <Text fontSize='medium' color='currentColor'>
                  <FormattedMessage id='shared_ui.form.load' />
                </Text>
              </ButtonUploadPhoto>
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
