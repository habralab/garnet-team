import { LogoutFlow }       from '@atls/next-identity-integration'

import React                from 'react'
import { FC }               from 'react'
import { FormattedMessage } from 'react-intl'
import { useState }         from 'react'
import { useIntl }          from 'react-intl'

import { Button }           from '@ui/button'
import { Condition }        from '@ui/condition'
import { LogoutIcon }       from '@ui/icon'
import { Layout }           from '@ui/layout'
import { Modal }            from '@ui/modal'
import { Text }             from '@ui/text'
import { useSession }       from '@stores/session'

export const ButtonExit: FC = () => {
  const [modalOpen, setModalOpen] = useState(false)
  const [isLogout, setIsLogout] = useState<boolean>(false)
  const { formatMessage } = useIntl()

  const { fullName, clearSession } = useSession()

  const toggleModalOpen = () => setModalOpen(!modalOpen)

  const handleExit = () => {
    toggleModalOpen()
    clearSession()
    setIsLogout(true)
  }

  return (
    <>
      <Condition match={!isLogout}>
        <Button variant='link' size='micro' onClick={toggleModalOpen}>
          <Text fontSize='medium' color='currentColor'>
            <FormattedMessage id='header.exit' />
          </Text>
          <LogoutIcon width={24} height={24} color='currentColor' />
        </Button>
        <Modal
          theme='exit'
          open={modalOpen}
          confirmText={formatMessage({ id: 'header.exit' })}
          onConfirm={handleExit}
          onCancel={toggleModalOpen}
          onClose={toggleModalOpen}
        >
          <Text fontSize='semiMedium' fontWeight='bold' color='text.secondary'>
            {fullName}
          </Text>
          <Layout flexBasis={4} flexShrink={0} />
          <Text fontSize='semiMedium' color='text.secondary'>
            <FormattedMessage id='header.exit_message' />
          </Text>
          <Layout flexBasis={40} />
        </Modal>
      </Condition>
      <Condition match={isLogout}>
        <Layout />
        <LogoutFlow />
      </Condition>
    </>
  )
}
