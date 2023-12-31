import React                    from 'react'
import { FC }                   from 'react'
import { useState }             from 'react'
import { useIntl }              from 'react-intl'

import { FormTeam }             from '@shared/forms-fragment'
import { FormTeamValues }       from '@shared/forms-fragment'
import { Layout }               from '@ui/layout'
import { Modal }                from '@ui/modal'

import { ModalCreateTeamProps } from './modal-create-team.interfaces'
import { getFormValues }        from '../helpers'
import { useSubmit }            from './hooks'

export const ModalCreateTeam: FC<ModalCreateTeamProps> = ({ modalOpen = false, onClose }) => {
  const [formValues, setFormValues] = useState<FormTeamValues>(getFormValues())
  const [submitDisabled, setSubmitDisabled] = useState<boolean>(true)
  const { formatMessage } = useIntl()

  const { submit, loading } = useSubmit()

  const closeModal = () => {
    setFormValues(getFormValues())
    onClose?.()
  }

  const handleChange = (field: keyof FormTeamValues) => (value: string | string[]) => {
    setFormValues({ ...formValues, [field]: value })
  }

  const handleSubmit = async () => {
    await submit(formValues)
    closeModal?.()
  }

  return (
    <Modal
      open={modalOpen}
      title={formatMessage({ id: 'shared_ui.modal.create_team' })}
      onClose={closeModal}
      onCancel={closeModal}
      onConfirm={handleSubmit}
      confirmProps={{ disabled: submitDisabled || loading }}
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
