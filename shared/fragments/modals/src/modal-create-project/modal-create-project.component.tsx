import React                       from 'react'
import { FC }                      from 'react'
import { useRouter }               from 'next/router'
import { useState }                from 'react'
import { useIntl }                 from 'react-intl'

import { FormProject }             from '@shared/forms-fragment'
import { FormProjectValues }       from '@shared/forms-fragment'
import { Layout }                  from '@ui/layout'
import { Modal }                   from '@ui/modal'

import { ModalCreateProjectProps } from './modal-create-project.interfaces'
import { getFormValues }           from '../helpers'
import { useCreateProject }        from './data'

export const ModalCreateProject: FC<ModalCreateProjectProps> = ({ modalOpen = false, onClose }) => {
  const [formValues, setFormValues] = useState<FormProjectValues>(getFormValues())
  const [submitDisabled, setSubmitDisabled] = useState<boolean>(true)
  const { formatMessage } = useIntl()

  const router = useRouter()

  const { createProject } = useCreateProject()

  const closeModal = () => {
    setFormValues(getFormValues())
    onClose?.()
  }

  const handleChange = (field: keyof FormProjectValues) => (value: string | string[]) => {
    setFormValues({ ...formValues, [field]: value })
  }

  const handleSubmit = async () => {
    try {
      const { avatar, description, name, tags } = formValues

      const avatarFile = await fetch(avatar).then((r) => r.blob())

      const { data } = await createProject({
        variables: {
          description,
          name,
          tags,
          avatar: avatarFile,
        },
      })

      router.push(`/project/${data?.projectCreate.id}`)
      closeModal?.()
    } catch (error) {
      /** @todo error notification */
    }
  }

  return (
    <Modal
      open={modalOpen}
      title={formatMessage({ id: 'shared_ui.modal.create_project' })}
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
