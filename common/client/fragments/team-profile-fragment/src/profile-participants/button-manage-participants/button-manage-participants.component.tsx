import React                             from 'react'
import { FC }                            from 'react'
import { useState }                      from 'react'

import { ModalParticipants }             from '@shared/modals-fragment'
import { Button }                        from '@ui/button'
import { Text }                          from '@ui/text'

import { ButtonManageParticipantsProps } from './button-manage-participants.interfaces'

export const ButtonManageParticipants: FC<ButtonManageParticipantsProps> = ({
  children,
  team,
  ...props
}) => {
  const [modalOpen, setModalOpen] = useState(false)

  const openModal = () => setModalOpen(true)
  const closeModal = () => setModalOpen(false)

  return (
    <>
      <Button variant='link' size='micro' onClick={openModal}>
        <Text fontSize='normal' color='currentColor'>
          {children}
        </Text>
      </Button>
      <ModalParticipants modalOpen={modalOpen} onClose={closeModal} title={team?.name} {...props} />
    </>
  )
}
