import React                   from 'react'
import { FC }                  from 'react'
import { FormattedMessage }    from 'react-intl'
import { useRef }              from 'react'
import { useState }            from 'react'

import { ModalEditImage }      from '@shared/modals-fragment'
import { Avatar }              from '@ui/avatar'
import { Button }              from '@ui/button'
import { Condition }           from '@ui/condition'
import { Input }               from '@ui/input'
import { Box }                 from '@ui/layout'
import { Layout }              from '@ui/layout'
import { Text }                from '@ui/text'

import { UploadPhotoProps }    from './upload-photo.interfaces'
import { useUploadUserAvatar } from '../data'

export const UploadPhoto: FC<UploadPhotoProps> = ({ onSubmit }) => {
  const [file, setFile] = useState<File>()
  const [blob, setBlob] = useState<Blob>()
  const [modalOpen, setModalOpen] = useState(false)
  const [avatarUrl, setAvatarUrl] = useState<string>()

  const inputRef = useRef<HTMLInputElement>(null)

  const { uploadUserAvatar } = useUploadUserAvatar()

  const toggleModalOpen = () => setModalOpen(!modalOpen)

  const handleClickUpload = () => {
    if (inputRef.current) {
      inputRef.current.value = ''
      inputRef.current.click()
    }
  }

  const handleUploadImage = (event) => {
    setFile(event.target?.files[0])
    toggleModalOpen()
  }

  const handleConfirm = (url?: string, blob?: Blob) => {
    setAvatarUrl(url)
    setBlob(blob)
    toggleModalOpen()
  }

  const handleSubmit = async () => {
    try {
      if (blob) {
        await uploadUserAvatar({ variables: { file: blob } })
        onSubmit?.()
      }
    } catch (error) {
      /** @todo error notification */
    }
  }

  return (
    <>
      <Box justifyContent='center'>
        <Text fontSize='extraHuge' fontWeight='superBold' color='text.secondary'>
          <FormattedMessage id='onboarding.upload_photo' />
        </Text>
      </Box>
      <Layout flexBasis={64} flexShrink={0} />
      <Box justifyContent='center' onClick={handleClickUpload}>
        <Box display='none'>
          <Input
            ref={inputRef}
            type='file'
            accept='image/*'
            hidden
            onChangeNative={handleUploadImage}
          />
        </Box>
        <Avatar image={avatarUrl} size={250} color='gray' style={{ cursor: 'pointer' }}>
          <Condition match={!avatarUrl}>
            <Text fontSize='medium' color='text.white'>
              <FormattedMessage id='onboarding.choose_photo' />
            </Text>
          </Condition>
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
        <Button variant='primary' size='normal' disabled={!avatarUrl} onClick={handleSubmit}>
          <Text fontSize='medium' color='currentColor'>
            <FormattedMessage id='onboarding.upload' />
          </Text>
        </Button>
      </Box>
      <ModalEditImage
        modalOpen={modalOpen}
        onClose={toggleModalOpen}
        onConfirm={handleConfirm}
        image={file && URL.createObjectURL(file)}
      />
    </>
  )
}
