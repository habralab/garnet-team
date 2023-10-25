import React                     from 'react'
import { FC }                    from 'react'
import { FormattedMessage }      from 'react-intl'
import { useState }              from 'react'

import { ModalCreateTeam }       from '@shared/modals-fragment'
import { Button }                from '@ui/button'
import { AddIcon }               from '@ui/icon'
import { Text }                  from '@ui/text'

import { ButtonCreateTeamProps } from './button-create-team.interfaces'

export const ButtonCreateTeam: FC<ButtonCreateTeamProps> = () => {
  const [modalOpen, setModalOpen] = useState(false)

  const openModal = () => setModalOpen(true)
  const closeModal = () => setModalOpen(false)

  return (
    <>
      <Button variant='secondary' size='small' onClick={openModal} style={{ paddingLeft: 20 }}>
        <AddIcon width={16} height={16} color='currentColor' />
        <Text fontSize='normal' color='currentColor'>
          <FormattedMessage id='profile.create_team' />
        </Text>
      </Button>
      <ModalCreateTeam modalOpen={modalOpen} onClose={closeModal} />
    </>
  )
}
