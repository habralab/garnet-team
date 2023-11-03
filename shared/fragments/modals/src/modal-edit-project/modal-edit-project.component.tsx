import React                      from 'react'
import { FC }                     from 'react'
import { useState }               from 'react'
import { useIntl }                from 'react-intl'

import { FormProject }            from '@shared/forms-fragment'
import { FormProjectValues }      from '@shared/forms-fragment'
import { Layout }                 from '@ui/layout'
import { Modal }                  from '@ui/modal'

import { ModalEditProjectProps }  from './modal-edit-project.interfaces'
import { getFormValues }          from '../helpers'
import { useUpdateProject }       from './data'
import { useUploadProjectAvatar } from './data'

export const ModalEditProject: FC<ModalEditProjectProps> = ({
  modalOpen = false,
  onClose,
  onSubmit,
  project,
}) => {
  const [formValues, setFormValues] = useState<FormProjectValues>(getFormValues(project))
  const [submitDisabled, setSubmitDisabled] = useState<boolean>(true)
  const { formatMessage } = useIntl()

  const { updateProject } = useUpdateProject()
  const { uploadProjectAvatar } = useUploadProjectAvatar()

  const closeModal = () => {
    setFormValues(getFormValues(project))
    onClose?.()
  }

  const handleChange = (field: keyof FormProjectValues) => (value: string | string[]) => {
    setFormValues({ ...formValues, [field]: value })
  }

  const handleSubmit = async () => {
    try {
      const { avatar, description, name, tags } = formValues

      if (project?.avatarUrl !== avatar) {
        const avatarFile = await fetch(avatar).then((r) => r.blob())

        await uploadProjectAvatar({ variables: { id: project?.id, file: avatarFile } })
      }

      await updateProject({ variables: { id: project?.id, name, description, tags } })

      onSubmit?.({
        ...project,
        projectName: name,
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
