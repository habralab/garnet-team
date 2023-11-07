import React                   from 'react'
import { FC }                  from 'react'
import { FormattedMessage }    from 'react-intl'
import { useState }            from 'react'

import { ModalEditTeam }       from '@shared/modals-fragment'
import { Button }              from '@ui/button'
import { Text }                from '@ui/text'

import { ButtonEditTeamProps } from './button-edit-team.interfaces'

export const ButtonEditTeam: FC<ButtonEditTeamProps> = ({ team, onEditTeam }) => {
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
      <ModalEditTeam modalOpen={modalOpen} onClose={closeModal} team={team} onSubmit={onEditTeam} />
    </>
  )
}
