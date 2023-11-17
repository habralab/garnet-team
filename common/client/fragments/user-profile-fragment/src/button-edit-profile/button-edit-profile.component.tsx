import React                      from 'react'
import { FC }                     from 'react'
import { FormattedMessage }       from 'react-intl'
import { useState }               from 'react'

import { ModalEditUser }          from '@shared/modals-fragment'
import { Button }                 from '@ui/button'
import { Text }                   from '@ui/text'

import { ButtonEditProfileProps } from './button-edit-profile.interfaces'

export const ButtonEditProfile: FC<ButtonEditProfileProps> = ({ user, onEditUser }) => {
  const [modalOpen, setModalOpen] = useState(false)

  const toggleModalOpen = () => setModalOpen(!modalOpen)

  return (
    <>
      <Button variant='link' size='micro' onClick={toggleModalOpen}>
        <Text fontSize='normal' color='currentColor'>
          <FormattedMessage id='profile.edit' />
        </Text>
      </Button>
      <ModalEditUser
        modalOpen={modalOpen}
        onClose={toggleModalOpen}
        user={user}
        onSubmit={onEditUser}
      />
    </>
  )
}
