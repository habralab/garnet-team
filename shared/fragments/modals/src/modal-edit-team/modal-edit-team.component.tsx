import React                   from 'react'
import { FC }                  from 'react'
import { useState }            from 'react'
import { useIntl }             from 'react-intl'

import { FormTeam }            from '@shared/forms-fragment'
import { FormTeamValues }      from '@shared/forms-fragment'
import { Layout }              from '@ui/layout'
import { Modal }               from '@ui/modal'

import { ModalEditTeamProps }  from './modal-edit-team.interfaces'
import { getFormValues }       from '../helpers'
import { useUpdateTeam }       from './data'
import { useUploadTeamAvatar } from './data'

export const ModalEditTeam: FC<ModalEditTeamProps> = ({
  modalOpen = false,
  onClose,
  team,
  onSubmit,
}) => {
  const [formValues, setFormValues] = useState<FormTeamValues>(getFormValues(team))
  const [submitDisabled, setSubmitDisabled] = useState<boolean>(true)
  const { formatMessage } = useIntl()

  const { updateTeam } = useUpdateTeam()
  const { uploadTeamAvatar } = useUploadTeamAvatar()

  const closeModal = () => {
    setFormValues(getFormValues(team))
    onClose?.()
  }

  const handleChange = (field: keyof FormTeamValues) => (value: string | string[]) => {
    setFormValues({ ...formValues, [field]: value })
  }

  const handleSubmit = async () => {
    try {
      const { avatar, description, name, tags } = formValues

      if (team?.avatarUrl !== avatar) {
        const avatarFile = await fetch(avatar).then((r) => r.blob())

        await uploadTeamAvatar({ variables: { id: team?.id, file: avatarFile } })
      }

      await updateTeam({ variables: { id: team?.id, name, description, tags } })

      onSubmit?.({
        ...team,
        name,
        description,
        tags,
        avatarUrl: avatar,
      })
      closeModal?.()
    } catch (error) {
      /** @todo error notification */
    }
  }

  return (
    <Modal
      open={modalOpen}
      title={formatMessage({ id: 'shared_ui.modal.edit_team' })}
      onClose={closeModal}
      onCancel={closeModal}
      onConfirm={handleSubmit}
      confirmProps={{ disabled: submitDisabled }}
    >
      <FormTeam
        formValues={formValues}
        handleChange={handleChange}
        handleDisabled={setSubmitDisabled}
      />
      <Layout flexBasis={50} flexShrink={0} />
    </Modal>
  )
}
