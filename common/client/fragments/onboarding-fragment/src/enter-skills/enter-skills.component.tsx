import React                from 'react'
import { FC }               from 'react'
import { FormattedMessage } from 'react-intl'
import { useIntl }          from 'react-intl'

import { Button }           from '@ui/button'
import { Multiselect }      from '@ui/input'
import { Box }              from '@ui/layout'
import { Layout }           from '@ui/layout'
import { Text }             from '@ui/text'

import { EnterSkillsProps } from './enter-skills.interfaces'
import { useSubmitSkills }  from '../hooks'
import { useSkillsState }   from './hooks'

export const EnterSkills: FC<EnterSkillsProps> = ({ onSubmit }) => {
  const { value, setValue, errorText, disabled } = useSkillsState()

  const { formatMessage } = useIntl()

  const { submit, loading } = useSubmitSkills()

  const handleSubmit = async () => {
    await submit(value)
    onSubmit?.()
  }

  return (
    <>
      <Box justifyContent='center'>
        <Text fontSize='extraHuge' fontWeight='superBold' color='text.secondary'>
          <FormattedMessage id='onboarding.add_skills' />
        </Text>
      </Box>
      <Layout flexBasis={64} flexShrink={0} />
      <Box>
        <Multiselect
          value={value}
          placeholder={formatMessage({ id: 'onboarding.enter_skill' })}
          onChange={setValue}
          errorText={errorText}
        />
      </Box>
      <Layout flexBasis={40} flexShrink={0} />
      <Box justifyContent='flex-end'>
        <Button
          variant='primary'
          size='normal'
          disabled={disabled || loading}
          onClick={handleSubmit}
        >
          <Text fontSize='medium' color='currentColor'>
            <FormattedMessage id='onboarding.save' />
          </Text>
        </Button>
      </Box>
    </>
  )
}
