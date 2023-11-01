import React                     from 'react'
import { FC }                    from 'react'
import { FormattedMessage }      from 'react-intl'
import { useState }              from 'react'
import { useIntl }               from 'react-intl'

import { Button }                from '@ui/button'
import { Textarea }              from '@ui/input'
import { Box }                   from '@ui/layout'
import { Layout }                from '@ui/layout'
import { Text }                  from '@ui/text'

import { EnterDescriptionProps } from './enter-description.interfaces'
import { sizes }                 from './enter-description.config'
import { validateValue }         from './helpers'

export const EnterDescription: FC<EnterDescriptionProps> = ({ onSubmit }) => {
  const [value, setValue] = useState<string>('')
  const [errorText, setErrorText] = useState<string>('')

  const { formatMessage } = useIntl()

  const handleChange = (newValue: string) => {
    setValue(newValue)

    if (value.length >= sizes.min && value.length < sizes.max) {
      setErrorText('')
    }
  }

  const updateErrorText = () => {
    validateValue(value, (id, values) => setErrorText(id ? formatMessage({ id }, values) : id))
  }

  const isSubmitDisabled = value.length < sizes.min || value.length > sizes.max

  const handleSubmit = () => onSubmit?.(value)

  return (
    <>
      <Box justifyContent='center'>
        <Text fontSize='extraHuge' fontWeight='superBold' color='text.secondary'>
          <FormattedMessage id='onboarding.add_information_about_yourself' />
        </Text>
      </Box>
      <Layout flexBasis={64} flexShrink={0} />
      <Textarea
        value={value}
        placeholder={formatMessage({ id: 'onboarding.information_about_yourself' })}
        onChange={handleChange}
        onBlur={updateErrorText}
        style={{ height: 152, resize: 'none' }}
        errorText={errorText}
      />
      <Layout flexBasis={10} flexShrink={0} />
      <Box justifyContent='flex-end'>
        <Text fontSize='normal' color='text.gray'>
          <FormattedMessage id='shared_ui.form.from_to_symbols' />
        </Text>
      </Box>
      <Layout flexBasis={40} flexShrink={0} />
      <Box justifyContent='flex-end'>
        <Button variant='primary' size='normal' disabled={isSubmitDisabled} onClick={handleSubmit}>
          <Text fontSize='medium' color='currentColor'>
            <FormattedMessage id='onboarding.further' />
          </Text>
        </Button>
      </Box>
    </>
  )
}
