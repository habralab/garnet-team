import React                               from 'react'
import { FC }                              from 'react'
import { useIntl }                         from 'react-intl'

import { ModalInvite }                     from '../modal-invite'
import { ModalInviteToProjectProps }       from './modal-invite-to-project.interfaces'
import { useCreateTeamJoinProjectRequest } from './data'

export const ModalInviteToProject: FC<ModalInviteToProjectProps> = ({
  invitedProjectId,
  modalOpen,
  onClose,
}) => {
  const { formatMessage } = useIntl()

  const { createTeamJoinProjectRequest, loading } = useCreateTeamJoinProjectRequest()

  const handleSubmit = async (teamId?: string) => {
    try {
      await createTeamJoinProjectRequest({
        variables: { projectId: invitedProjectId || '', teamId: teamId || '' },
      })
      onClose?.()
    } catch (error) {
      /** @todo error notification */
    }
  }

  return (
    <ModalInvite
      title={formatMessage({ id: 'shared_ui.modal.invite_to_project' })}
      modalOpen={modalOpen}
      loading={loading}
      onClose={onClose}
      onSubmit={handleSubmit}
    />
  )
}
