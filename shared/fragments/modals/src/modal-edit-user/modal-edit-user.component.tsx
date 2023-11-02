import React                  from 'react'
import { FC }                 from 'react'
import { useState }           from 'react'
import { useIntl }            from 'react-intl'

import { FormUser }           from '@shared/forms-fragment'
import { FormUserValues }     from '@shared/forms-fragment'
import { Layout }             from '@ui/layout'
import { Modal }              from '@ui/modal'

import { ModalEditUserProps } from './modal-edit-user.interfaces'
import { getFormValues }      from './helpers'

export const ModalEditUser: FC<ModalEditUserProps> = ({ modalOpen = false, onClose, user }) => {
  const [formValues, setFormValues] = useState<FormUserValues>(getFormValues(user))
  const [submitDisabled, setSubmitDisabled] = useState<boolean>(true)
  const { formatMessage } = useIntl()

  const closeModal = () => {
    setFormValues(getFormValues(user))
    onClose?.()
  }

  const handleChange = (field: keyof FormUserValues) => (value: string | string[]) => {
    setFormValues({ ...formValues, [field]: value })
  }

  const handleSubmit = () => {
    /** @todo submit form */
    closeModal?.()
  }

  return (
    <Modal
      open={modalOpen}
      title={formatMessage({ id: 'shared_ui.modal.edit_profile' })}
      onClose={closeModal}
      onCancel={closeModal}
      onConfirm={handleSubmit}
      confirmProps={{ disabled: submitDisabled }}
    >
      <FormUser
        formValues={formValues}
        handleChange={handleChange}
        handleDisabled={setSubmitDisabled}
      />
      <Layout flexBasis={50} flexShrink={0} />
    </Modal>
  )
}
