import React                 from 'react'
import { FC }                from 'react'
import { FormattedMessage }  from 'react-intl'
import { useState }          from 'react'

import { ModalInviteToTeam } from '@shared/modals-fragment'
import { Button }            from '@ui/button'
import { Text }              from '@ui/text'

import { ButtonInviteProps } from './button-invite.interfaces'

export const ButtonInvite: FC<ButtonInviteProps> = ({ user }) => {
  const [modalOpen, setModalOpen] = useState(false)

  const toggleModalOpen = () => setModalOpen(!modalOpen)

  return (
    <>
      <Button variant='primary' size='normal' onClick={toggleModalOpen}>
        <Text fontSize='medium' color='currentColor'>
          <FormattedMessage id='profile.invite' />
        </Text>
      </Button>
      <ModalInviteToTeam modalOpen={modalOpen} onClose={toggleModalOpen} invitedUserId={user?.id} />
    </>
  )
}
