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
import { useSubmit }             from './hooks'

export const ModalEditProject: FC<ModalEditProjectProps> = ({
  modalOpen = false,
  onClose,
  onSubmit,
  project,
}) => {
  const [formValues, setFormValues] = useState<FormProjectValues>(getFormValues(project))
  const [submitDisabled, setSubmitDisabled] = useState<boolean>(true)
  const { formatMessage } = useIntl()

  const { submit } = useSubmit(project)

  const closeModal = () => {
    setFormValues(getFormValues(project))
    onClose?.()
  }

  const handleChange = (field: keyof FormProjectValues) => (value: string | string[]) => {
    setFormValues({ ...formValues, [field]: value })
  }

  const handleSubmit = async () => {
    const editedProject = await submit(formValues)

    if (editedProject) {
      onSubmit?.(editedProject)
      closeModal?.()
    }
  }

  return (
    <Modal
      open={modalOpen}
      title={formatMessage({ id: 'shared_ui.modal.edit_project' })}
      onClose={closeModal}
      onCancel={closeModal}
      onConfirm={handleSubmit}
      confirmProps={{ disabled: submitDisabled }}
    >
      <FormProject
        formValues={formValues}
        handleChange={handleChange}
        handleDisabled={setSubmitDisabled}
      />
      <Layout flexBasis={50} flexShrink={0} />
    </Modal>
  )
}
