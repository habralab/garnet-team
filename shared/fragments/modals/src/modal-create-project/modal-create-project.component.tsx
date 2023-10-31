import React                       from 'react'
import { FC }                      from 'react'
import { useState }                from 'react'
import { useIntl }                 from 'react-intl'

import { FormProject }             from '@ui/form'
import { FormProjectValues }       from '@ui/form'
import { Layout }                  from '@ui/layout'
import { Modal }                   from '@ui/modal'

import { ModalCreateProjectProps } from './modal-create-project.interfaces'
import { getFormValues }           from '../helpers'

export const ModalCreateProject: FC<ModalCreateProjectProps> = ({ modalOpen = false, onClose }) => {
  const [formValues, setFormValues] = useState<FormProjectValues>(getFormValues())
  const { formatMessage } = useIntl()

  const closeModal = () => {
    setFormValues(getFormValues())
    onClose?.()
  }

  const handleChange = (field: keyof FormProjectValues) => (value: string | string[]) => {
    setFormValues({ ...formValues, [field]: value })
  }

  const handleSubmit = () => {
    /** @todo submit form */
    closeModal()
  }

  return (
    <Modal
      open={modalOpen}
      title={formatMessage({ id: 'shared_ui.modal.create_project' })}
      onClose={closeModal}
      onCancel={closeModal}
      onConfirm={handleSubmit}
    >
      <FormProject formValues={formValues} handleChange={handleChange} />
      <Layout flexBasis={50} flexShrink={0} />
    </Modal>
  )
}
