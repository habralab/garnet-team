import React                    from 'react'
import { FC }                   from 'react'
import { useState }             from 'react'
import { useIntl }              from 'react-intl'

import { FormTeam }             from '@ui/form'
import { FormTeamValues }       from '@ui/form'
import { Layout }               from '@ui/layout'
import { Modal }                from '@ui/modal'

import { ModalCreateTeamProps } from './modal-create-team.interfaces'
import { getFormValues }        from '../helpers'

export const ModalCreateTeam: FC<ModalCreateTeamProps> = ({ modalOpen = false, onClose }) => {
  const [formValues, setFormValues] = useState<FormTeamValues>(getFormValues())
  const { formatMessage } = useIntl()

  const closeModal = () => {
    setFormValues(getFormValues())
    onClose?.()
  }

  const handleChange = (field: keyof FormTeamValues) => (value: string | string[]) => {
    setFormValues({ ...formValues, [field]: value })
  }

  const handleSubmit = () => {
    /** @todo submit form */
    closeModal()
  }

  return (
    <Modal
      open={modalOpen}
      title={formatMessage({ id: 'shared_ui.modal.create_team' })}
      onClose={closeModal}
      onCancel={closeModal}
      onOk={handleSubmit}
    >
      <FormTeam formValues={formValues} handleChange={handleChange} />
      <Layout flexBasis={50} flexShrink={0} />
    </Modal>
  )
}
