import { Background }       from '@ui/background'
import { Button }           from '@ui/button'
import { useRouter }        from 'next/router'
import React                from 'react'
import { Column, Layout, Row }   from '@ui/layout'
import { FormattedMessage } from 'react-intl'
import { Text }             from '@ui/text'

export const ConfirmationCard = () => {
  const { replace } = useRouter()

  const handleClick = () => replace('/auth/login')

  return (
    <Background color='white' borderRadius='medium' boxShadow='black'>
      <Column paddingX={32} alignItems='center' width={342} boxSizing='content-box'>
        <Layout flexBasis={56}/>
        <Text fontSize='preLarge' fontWeight='bold' textAlign='center'>
          <FormattedMessage id='confirmation.success'/>
        </Text>
        <Layout flexBasis={40} flexShrink={0}/>
        <Row justifyContent='center'>
          <Button onClick={handleClick}>
            <FormattedMessage id='confirmation.enter'/>
          </Button>
        </Row>
        <Layout flexBasis={56}/>
      </Column>
    </Background>
  )
}
