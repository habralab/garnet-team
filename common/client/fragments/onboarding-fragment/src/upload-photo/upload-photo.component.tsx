import React                from 'react'
import { FC }               from 'react'
import { FormattedMessage } from 'react-intl'

import { Avatar }           from '@ui/avatar'
import { Button }           from '@ui/button'
import { Box }              from '@ui/layout'
import { Layout }           from '@ui/layout'
import { Text }             from '@ui/text'

import { UploadPhotoProps } from './upload-photo.interfaces'

export const UploadPhoto: FC<UploadPhotoProps> = ({ onSubmit }) => {
  const handleSubmit = () => onSubmit?.()

  return (
    <>
      <Box justifyContent='center'>
        <Text fontSize='extraHuge' fontWeight='superBold' color='text.secondary'>
          <FormattedMessage id='onboarding.upload_photo' />
        </Text>
      </Box>
      <Layout flexBasis={64} flexShrink={0} />
      <Box justifyContent='center'>
        <Avatar size={250} color='gray' style={{ cursor: 'pointer' }}>
          <Text fontSize='medium' color='text.white'>
            <FormattedMessage id='onboarding.choose_photo' />
          </Text>
        </Avatar>
      </Box>
      <Layout flexBasis={20} flexShrink={0} />
      <Box justifyContent='center'>
        <Text fontSize='normal' color='text.gray'>
          <FormattedMessage id='onboarding.minimum_size' />
        </Text>
      </Box>
      <Layout flexBasis={40} flexShrink={0} />
      <Box justifyContent='center'>
        <Button variant='primary' size='normal' onClick={handleSubmit}>
          <Text fontSize='medium' color='currentColor'>
            <FormattedMessage id='onboarding.upload' />
          </Text>
        </Button>
      </Box>
    </>
  )
}
