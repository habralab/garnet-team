import React                  from 'react'
import { FC }                 from 'react'
import { useState }           from 'react'
import { useIntl }            from 'react-intl'

import { FormTeam }           from '@shared/forms-fragment'
import { FormTeamValues }     from '@shared/forms-fragment'
import { Layout }             from '@ui/layout'
import { Modal }              from '@ui/modal'

import { ModalEditTeamProps } from './modal-edit-team.interfaces'
import { getFormValues }      from '../helpers'
import { useSubmit }          from './hooks'

export const ModalEditTeam: FC<ModalEditTeamProps> = ({
  modalOpen = false,
  onClose,
  team,
  onSubmit,
}) => {
  const [formValues, setFormValues] = useState<FormTeamValues>(getFormValues(team))
  const [submitDisabled, setSubmitDisabled] = useState<boolean>(true)
  const { formatMessage } = useIntl()

  const { submit } = useSubmit(team)

  const closeModal = () => {
    setFormValues(getFormValues(team))
    onClose?.()
  }

  const handleChange = (field: keyof FormTeamValues) => (value: string | string[]) => {
    setFormValues({ ...formValues, [field]: value })
  }

  const handleSubmit = async () => {
    const editedTeam = await submit(formValues)

    if (editedTeam) {
      onSubmit?.(editedTeam)
      closeModal?.()
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
