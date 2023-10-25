import React                      from 'react'
import { FC }                     from 'react'
import { FormattedMessage }       from 'react-intl'
import { useState }               from 'react'

import { ModalEditProject }       from '@shared/modals-fragment'
import { Button }                 from '@ui/button'
import { Text }                   from '@ui/text'

import { ButtonEditProjectProps } from './button-edit-project.interfaces'

export const ButtonEditProject: FC<ButtonEditProjectProps> = ({ project }) => {
  const [modalOpen, setModalOpen] = useState(false)

  const openModal = () => setModalOpen(true)
  const closeModal = () => setModalOpen(false)

  return (
    <>
      <Button variant='link' size='micro' onClick={openModal}>
        <Text fontSize='medium' color='currentColor'>
          <FormattedMessage id='profile.change' />
        </Text>
      </Button>
      <ModalEditProject modalOpen={modalOpen} onClose={closeModal} project={project} />
    </>
  )
}
