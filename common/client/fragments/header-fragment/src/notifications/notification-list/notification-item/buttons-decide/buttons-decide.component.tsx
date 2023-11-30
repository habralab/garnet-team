import React                  from 'react'
import { FC }                 from 'react'
import { FormattedMessage }   from 'react-intl'

import { Button }             from '@ui/button'
import { Layout }             from '@ui/layout'
import { Text }               from '@ui/text'

import { ButtonsDecideProps } from './buttons-decide.interfaces'
import { useDecideRequest }   from '../../../../hooks'

export const ButtonsDecide: FC<ButtonsDecideProps> = ({ linkedRequestId, type }) => {
  const { decideRequest, loading } = useDecideRequest(linkedRequestId, type)

  const handleDecide = (approve: boolean) => () => decideRequest(approve)

  return (
    <>
      <Button variant='link' size='micro' disabled={loading} onClick={handleDecide(false)}>
        <Text fontSize='normal' color='currentColor'>
          <FormattedMessage id='header.notifications.reject' />
        </Text>
      </Button>
      <Layout flexBasis={28} flexShrink={0} />
      <Button variant='secondary' size='small' disabled={loading} onClick={handleDecide(true)}>
        <Text fontSize='normal' color='currentColor'>
          <FormattedMessage id='header.notifications.accept' />
        </Text>
      </Button>
    </>
  )
}
