import React                  from 'react'
import { FC }                 from 'react'
import { useState }           from 'react'
import { useIntl }            from 'react-intl'

import { FormUser }           from '@ui/form'
import { FormUserValues }     from '@ui/form'
import { Layout }             from '@ui/layout'
import { Modal }              from '@ui/modal'

import { ModalEditUserProps } from './modal-edit-user.interfaces'
import { getFormValues }      from './helpers'

export const ModalEditUser: FC<ModalEditUserProps> = ({ modalOpen = false, onClose, user }) => {
  const [formValues, setFormValues] = useState<FormUserValues>(getFormValues(user))
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
    >
      <FormUser formValues={formValues} handleChange={handleChange} />
      <Layout flexBasis={50} flexShrink={0} />
    </Modal>
  )
}
