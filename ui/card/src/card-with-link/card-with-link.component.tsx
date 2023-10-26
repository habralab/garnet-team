import React                 from 'react'
import { FC }                from 'react'
import { useRouter }         from 'next/router'

import { Link }              from '@ui/link'

import { Card }              from '../card.component'
import { CardWithLinkProps } from './card-with-link.interfaces'

export const CardWithLink: FC<CardWithLinkProps> = ({ url, ...props }) => {
  const router = useRouter()

  if (url) {
    const handleClick = (event) => {
      event.preventDefault()

      router.push(url)
    }

    return (
      <Link href={url} onClick={handleClick}>
        <Card {...props} />
      </Link>
    )
  }

  return <Card {...props} />
}
