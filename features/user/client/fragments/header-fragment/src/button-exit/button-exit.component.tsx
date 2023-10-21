import React                from 'react'
import { FC }               from 'react'
import { FormattedMessage } from 'react-intl'
import { useState }         from 'react'

import { Background }       from '@ui/background'
import { Button }           from '@ui/button'
import { LogoutIcon }       from '@ui/icon'
import { Layout }           from '@ui/layout'
import { Row }              from '@ui/layout'
import { Column }           from '@ui/layout'
import { Box }              from '@ui/layout'
import { Modal }            from '@ui/modal'
import { Text }             from '@ui/text'

const mockUserName = 'Маша Петрова'

export const ButtonExit: FC = () => {
  const [modalOpen, setModalOpen] = useState(false)

  const closeModal = () => setModalOpen(false)
  const openModal = () => setModalOpen(true)

  const handleExit = () => {
    /** @todo logout user */
    setModalOpen(true)
  }

  return (
    <Box fill justifyContent='flex-end'>
      <Button variant='link' onClick={openModal}>
        <Text fontSize='medium' color='currentColor'>
          <FormattedMessage id='header.exit' />
        </Text>
        <LogoutIcon width={24} height={24} color='currentColor' />
      </Button>
      <Modal open={modalOpen} onClose={closeModal}>
        <Background
          position='absolute'
          top={140}
          right={24}
          fill
          maxWidth={350}
          maxHeight={200}
          borderRadius='medium'
          color='white'
          alignItems='center'
          justifyContent='center'
        >
          <Layout flexBasis={60} />
          <Column alignItems='center'>
            <Layout flexBasis={40} />
            <Text fontSize='semiMedium' fontWeight='bold' color='text.secondary'>
              {mockUserName}
            </Text>
            <Layout flexBasis={4} flexShrink={0} />
            <Text fontSize='semiMedium' color='text.secondary'>
              <FormattedMessage id='header.exit_message' />
            </Text>
            <Layout flexBasis={40} />
            <Row justifyContent='space-between'>
              <Button variant='link' onClick={closeModal}>
                <FormattedMessage id='header.cancel' />
              </Button>
              <Button variant='secondary' onClick={handleExit}>
                <FormattedMessage id='header.exit' />
              </Button>
            </Row>
          </Column>
          <Layout flexBasis={60} />
        </Background>
      </Modal>
      <Layout flexBasis={24} flexShrink={0} />
    </Box>
  )
}
