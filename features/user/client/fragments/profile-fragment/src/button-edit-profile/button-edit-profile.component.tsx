import React                      from 'react'
import { FC }                     from 'react'
import { FormattedMessage }       from 'react-intl'
import { useState }               from 'react'
import { useIntl }                from 'react-intl'

import { Button }                 from '@ui/button'
import { FormUser }               from '@ui/form'
import { FormUserValues }         from '@ui/form'
import { Layout }                 from '@ui/layout'
import { Modal }                  from '@ui/modal'
import { Text }                   from '@ui/text'

import { ButtonEditProfileProps } from './button-edit-profile.interfaces'
import { getFormValues }          from './helpers'

export const ButtonEditProfile: FC<ButtonEditProfileProps> = ({ user }) => {
  const [modalOpen, setModalOpen] = useState(false)
  const [formValues, setFormValues] = useState<FormUserValues>(getFormValues(user))
  const { formatMessage } = useIntl()

  const openModal = () => setModalOpen(true)
  const closeModal = () => {
    setFormValues(getFormValues(user))
    setModalOpen(false)
  }

  const handleChange = (field: keyof FormUserValues) => (value: string) => {
    setFormValues({ ...formValues, [field]: value })
  }

  const handleSubmit = () => {
    /** @todo submit form */
    closeModal()
  }

  return (
    <>
      <Button variant='link' size='micro' onClick={openModal}>
        <Text fontSize='normal' color='currentColor'>
          <FormattedMessage id='profile.edit' />
        </Text>
      </Button>
      <Modal
        open={modalOpen}
        title={formatMessage({ id: 'profile.edit_profile' })}
        onClose={closeModal}
        onCancel={closeModal}
        onOk={handleSubmit}
      >
        <FormUser formValues={formValues} handleChange={handleChange} />
        <Layout flexBasis={50} flexShrink={0} />
      </Modal>
    </>
  )
}
