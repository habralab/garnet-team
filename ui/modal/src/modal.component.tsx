import { Modal as ModalComponent } from '@atls-ui-proto/modal'

import React                       from 'react'
import { FC }                      from 'react'
import { useIntl }                 from 'react-intl'

import { Background }              from '@ui/background'
import { Button }                  from '@ui/button'
import { Condition }               from '@ui/condition'
import { ClearIcon }               from '@ui/icon'
import { Box }                     from '@ui/layout'
import { Column }                  from '@ui/layout'
import { Row }                     from '@ui/layout'
import { Layout }                  from '@ui/layout'
import { Text }                    from '@ui/text'

import { ModalProps }              from './modal.interfaces'

export const Modal: FC<ModalProps> = ({
  children,
  theme = 'primary',
  title,
  okText,
  cancelText,
  showOk = true,
  showCancel = true,
  onOk,
  onCancel,
  onClose,
  ...props
}) => {
  const { formatMessage } = useIntl()

  const defaultCancelText = cancelText ?? formatMessage({ id: 'shared_ui.cancel' })
  const defaultOkText = okText ?? formatMessage({ id: 'shared_ui.save' })

  return (
    <ModalComponent onClose={onClose} {...props}>
      <Condition match={theme === 'primary'}>
        <Background
          position='absolute'
          top='50%'
          left='50%'
          style={{ transform: 'translate(-50%, -50%)' }}
          maxWidth={632}
          borderRadius='medium'
          color='white'
          alignItems='center'
        >
          <Box
            position='absolute'
            top={16}
            right={16}
            style={{ cursor: 'pointer' }}
            onClick={onClose}
          >
            <ClearIcon width={32} height={32} />
          </Box>
          <Layout flexBasis={32} flexShrink={0} />
          <Column alignItems='center'>
            <Layout flexBasis={56} />
            <Condition match={Boolean(title)}>
              <Text fontSize='preLarge' fontWeight='bold' color='text.secondary'>
                {title}
              </Text>
              <Layout flexBasis={50} flexShrink={0} />
            </Condition>
            {children}
            <Row justifyContent='flex-end'>
              <Condition match={showCancel}>
                <Button variant='link' onClick={onCancel}>
                  {defaultCancelText}
                </Button>
                <Layout flexBasis={24} flexShrink={0} />
              </Condition>
              <Condition match={showOk}>
                <Button variant='primary' onClick={onOk}>
                  {defaultOkText}
                </Button>
              </Condition>
            </Row>
            <Layout flexBasis={56} />
          </Column>
          <Layout flexBasis={32} flexShrink={0} />
        </Background>
      </Condition>
      <Condition match={theme === 'exit'}>
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
            {children}
            <Row justifyContent='space-between'>
              <Condition match={showCancel}>
                <Button variant='link' onClick={onCancel}>
                  {defaultCancelText}
                </Button>
              </Condition>
              <Condition match={showOk}>
                <Button variant='secondary' onClick={onOk}>
                  {defaultOkText}
                </Button>
              </Condition>
            </Row>
            <Layout flexBasis={40} />
          </Column>
          <Layout flexBasis={60} />
        </Background>
      </Condition>
    </ModalComponent>
  )
}
