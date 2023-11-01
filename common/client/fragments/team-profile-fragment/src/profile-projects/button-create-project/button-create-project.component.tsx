import React                        from 'react'
import { FC }                       from 'react'
import { FormattedMessage }         from 'react-intl'
import { useState }                 from 'react'

import { ModalCreateProject }       from '@shared/modals-fragment'
import { Button }                   from '@ui/button'
import { AddIcon }                  from '@ui/icon'
import { Text }                     from '@ui/text'

import { ButtonCreateProjectProps } from './button-create-project.interfaces'

export const ButtonCreateProject: FC<ButtonCreateProjectProps> = () => {
  const [modalOpen, setModalOpen] = useState(false)

  const openModal = () => setModalOpen(true)
  const closeModal = () => setModalOpen(false)

  return (
    <>
      <Button
        variant='secondary'
        size='small'
        onClick={openModal}
        horizontalLocation='left'
        iconSvg={<AddIcon width={16} height={16} color='currentColor' />}
      >
        <Text fontSize='normal' color='currentColor'>
          <FormattedMessage id='profile.create_project' />
        </Text>
      </Button>
      <ModalCreateProject modalOpen={modalOpen} onClose={closeModal} />
    </>
  )
}
