import React                       from 'react'
import { FC }                      from 'react'
import { useIntl }                 from 'react-intl'

import { ModalInvite }             from '../modal-invite'
import { ModalInviteToTeamProps }  from './modal-invite-to-team.interfaces'
import { useCreateTeamJoinInvite } from './data'

export const ModalInviteToTeam: FC<ModalInviteToTeamProps> = ({
  invitedUserId,
  modalOpen,
  onClose,
}) => {
  const { formatMessage } = useIntl()

  const { createTeamJoiInvite, loading } = useCreateTeamJoinInvite()

  const handleSubmit = async (teamId?: string) => {
    try {
      await createTeamJoiInvite({
        variables: { userId: invitedUserId || '', teamId: teamId || '' },
      })
      onClose?.()
    } catch (error) {
      /** @todo error notification */
    }
  }

  return (
    <ModalInvite
      title={formatMessage({ id: 'shared_ui.modal.invite_to_team' })}
      modalOpen={modalOpen}
      loading={loading}
      onClose={onClose}
      onSubmit={handleSubmit}
    />
  )
}
