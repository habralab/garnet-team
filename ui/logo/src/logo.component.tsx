import React         from 'react'
import { FC }        from 'react'
import { useRouter } from 'next/router'

import { NextLink }  from '@ui/link'
import { Link }      from '@ui/link'

import { Icon }      from './icon.component'

export const Logo: FC = () => {
  const router = useRouter()

  if (router.route === '/') {
    return (
      // eslint-disable-next-line
      <Link href='#' width='100%'>
        <Icon />
      </Link>
    )
  }

  return (
    <NextLink path='/' width='100%'>
      <Icon />
    </NextLink>
  )
}
