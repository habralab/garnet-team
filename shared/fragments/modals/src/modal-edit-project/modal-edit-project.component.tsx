import React                     from 'react'
import { FC }                    from 'react'
import { useState }              from 'react'
import { useIntl }               from 'react-intl'

import { FormProject }           from '@shared/forms-fragment'
import { FormProjectValues }     from '@shared/forms-fragment'
import { Layout }                from '@ui/layout'
import { Modal }                 from '@ui/modal'

import { ModalEditProjectProps } from './modal-edit-project.interfaces'
import { getFormValues }         from '../helpers'

export const ModalEditProject: FC<ModalEditProjectProps> = ({
  modalOpen = false,
  onClose,
  project,
}) => {
  const [formValues, setFormValues] = useState<FormProjectValues>(getFormValues(project))
  const { formatMessage } = useIntl()

  const closeModal = () => {
    setFormValues(getFormValues(project))
    onClose?.()
  }

  const handleChange = (field: keyof FormProjectValues) => (value: string | string[]) => {
    setFormValues({ ...formValues, [field]: value })
  }

  const handleSubmit = () => {
    /** @todo submit form */
    closeModal?.()
  }

  return (
    <Modal
      open={modalOpen}
      title={formatMessage({ id: 'shared_ui.modal.edit_project' })}
      onClose={closeModal}
      onCancel={closeModal}
      onConfirm={handleSubmit}
    >
      <FormProject formValues={formValues} handleChange={handleChange} />
      <Layout flexBasis={50} flexShrink={0} />
    </Modal>
  )
}
