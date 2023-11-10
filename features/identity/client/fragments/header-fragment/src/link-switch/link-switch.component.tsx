import React                from 'react'
import { FC }               from 'react'
import { FormattedMessage } from 'react-intl'
import { useRouter }        from 'next/router'

import { Layout }           from '@ui/layout'
import { Box }              from '@ui/layout'
import { NextLink }         from '@ui/link'

import { getPathAndText }   from './get-path-and-text'

export const LinkSwitch: FC = () => {
  const { asPath } = useRouter()
  const { redirect, text } = getPathAndText(asPath)

  return (
    <Box fill justifyContent='flex-end' alignItems='center'>
      <NextLink path={redirect} href={redirect}>
        <FormattedMessage id={text} />
      </NextLink>
      <Layout flexBasis={40} />
    </Box>
  )
}
