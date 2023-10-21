import React                from 'react'
import { FC }               from 'react'
import { FormattedMessage } from 'react-intl'
import { useState }         from 'react'
import { useIntl }          from 'react-intl'

import { Button }           from '@ui/button'
import { LogoutIcon }       from '@ui/icon'
import { Layout }           from '@ui/layout'
import { Box }              from '@ui/layout'
import { Modal }            from '@ui/modal'
import { Text }             from '@ui/text'

const mockUserName = 'Маша Петрова'

export const ButtonExit: FC = () => {
  const [modalOpen, setModalOpen] = useState(false)
  const { formatMessage } = useIntl()

  const closeModal = () => setModalOpen(false)
  const openModal = () => setModalOpen(true)

  const handleExit = () => {
    /** @todo logout user */
    closeModal()
  }

  return (
    <Box fill justifyContent='flex-end'>
      <Button variant='link' onClick={openModal}>
        <Text fontSize='medium' color='currentColor'>
          <FormattedMessage id='header.exit' />
        </Text>
        <LogoutIcon width={24} height={24} color='currentColor' />
      </Button>
      <Modal
        theme='exit'
        open={modalOpen}
        okText={formatMessage({ id: 'header.exit' })}
        onOk={handleExit}
        onCancel={closeModal}
        onClose={closeModal}
      >
        <Text fontSize='semiMedium' fontWeight='bold' color='text.secondary'>
          {mockUserName}
        </Text>
        <Layout flexBasis={4} flexShrink={0} />
        <Text fontSize='semiMedium' color='text.secondary'>
          <FormattedMessage id='header.exit_message' />
        </Text>
        <Layout flexBasis={40} />
      </Modal>
      <Layout flexBasis={24} flexShrink={0} />
    </Box>
  )
}
