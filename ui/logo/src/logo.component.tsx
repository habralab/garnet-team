import React         from 'react'
import { FC }        from 'react'
import { useRouter } from 'next/router'

import { NextLink }  from '@ui/link'
import { Link }      from '@ui/link'
import { routes }    from '@shared/routes'

import { Icon }      from './icon.component'
import { LogoProps } from './logo.interfaces'

export const Logo: FC<LogoProps> = ({ withLink = true }) => {
  const router = useRouter()

  if (!withLink) {
    return <Icon />
  }

  if (router.route === routes.root) {
    return (
      // eslint-disable-next-line
      <Link href='#' width='100%'>
        <Icon />
      </Link>
    )
  }

  return (
    <NextLink path={routes.root} width='100%'>
      <Icon />
    </NextLink>
  )
}
