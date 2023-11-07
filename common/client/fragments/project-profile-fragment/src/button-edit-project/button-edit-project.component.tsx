import React                      from 'react'
import { FC }                     from 'react'
import { FormattedMessage }       from 'react-intl'
import { useState }               from 'react'

import { ModalEditProject }       from '@shared/modals-fragment'
import { Button }                 from '@ui/button'
import { Text }                   from '@ui/text'

import { ButtonEditProjectProps } from './button-edit-project.interfaces'

export const ButtonEditProject: FC<ButtonEditProjectProps> = ({ project, onEditProject }) => {
  const [modalOpen, setModalOpen] = useState(false)

  const toggleModalOpen = () => setModalOpen(!modalOpen)

  return (
    <>
      <Button variant='link' size='micro' onClick={toggleModalOpen}>
        <Text fontSize='medium' color='currentColor'>
          <FormattedMessage id='profile.change' />
        </Text>
      </Button>
      <ModalEditProject
        modalOpen={modalOpen}
        onClose={toggleModalOpen}
        project={project}
        onSubmit={onEditProject}
      />
    </>
  )
}
