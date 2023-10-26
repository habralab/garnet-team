import React                        from 'react'
import { FC }                       from 'react'
import { FormattedMessage }         from 'react-intl'
import { useState }                 from 'react'
import { useIntl }                  from 'react-intl'

import { Button }                   from '@ui/button'
import { FormProject }              from '@ui/form'
import { FormProjectValues }        from '@ui/form'
import { AddIcon }                  from '@ui/icon'
import { Layout }                   from '@ui/layout'
import { Modal }                    from '@ui/modal'
import { Text }                     from '@ui/text'

import { ButtonCreateProjectProps } from './button-create-project.interfaces'

const defaultFormValues: FormProjectValues = { name: '', description: '', tags: [], avatar: '' }

export const ButtonCreateProject: FC<ButtonCreateProjectProps> = () => {
  const [modalOpen, setModalOpen] = useState(false)
  const [formValues, setFormValues] = useState<FormProjectValues>(defaultFormValues)
  const { formatMessage } = useIntl()

  const openModal = () => setModalOpen(true)
  const closeModal = () => {
    setFormValues(defaultFormValues)
    setModalOpen(false)
  }

  const handleChange = (field: keyof FormProjectValues) => (value: string | string[]) => {
    setFormValues({ ...formValues, [field]: value })
  }

  const handleSubmit = () => {
    /** @todo submit form */
    closeModal()
  }

  return (
    <>
      <Button variant='secondary' size='small' onClick={openModal} style={{ paddingLeft: 20 }}>
        <AddIcon width={16} height={16} color='currentColor' />
        <Text fontSize='normal' color='currentColor'>
          <FormattedMessage id='profile.create_project' />
        </Text>
      </Button>
      <Modal
        open={modalOpen}
        title={formatMessage({ id: 'profile.create_project' })}
        onClose={closeModal}
        onCancel={closeModal}
        onOk={handleSubmit}
      >
        <FormProject formValues={formValues} handleChange={handleChange} />
        <Layout flexBasis={50} flexShrink={0} />
      </Modal>
    </>
  )
}
