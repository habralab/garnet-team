import React                      from 'react'
import { ChangeEvent }            from 'react'
import { FC }                     from 'react'
import { useRef }                 from 'react'
import { useState }               from 'react'

import { ModalEditImage }         from '@shared/modals-fragment'
import { Button }                 from '@ui/button'
import { HiddenInput }            from '@ui/input'

import { ButtonUploadPhotoProps } from './button-upload-photo.interfaces'

export const ButtonUploadPhoto: FC<ButtonUploadPhotoProps> = ({
  children,
  onChange,
  shape = 'circle',
}) => {
  const [file, setFile] = useState<File>()
  const [modalOpen, setModalOpen] = useState(false)

  const inputRef = useRef<HTMLInputElement>(null)

  const toggleModalOpen = () => setModalOpen(!modalOpen)

  const handleUploadImage = (event: ChangeEvent<HTMLInputElement>) => {
    setFile(event.target.files?.[0])
    toggleModalOpen()
  }

  const handleConfirm = (url?: string) => {
    onChange?.(url || '')
    toggleModalOpen()
  }

  const handleClickUpload = () => {
    if (inputRef.current) {
      inputRef.current.value = ''
      inputRef.current.click()
    }
  }

  return (
    <>
      <Button variant='link' size='micro' onClick={handleClickUpload}>
        {children}
      </Button>
      <HiddenInput ref={inputRef} type='file' accept='image/*' onChange={handleUploadImage} />
      <ModalEditImage
        modalOpen={modalOpen}
        onClose={toggleModalOpen}
        onConfirm={handleConfirm}
        shape={shape}
        image={file && URL.createObjectURL(file)}
      />
    </>
  )
}
