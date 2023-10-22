import { createNextNavLink } from '@atls-ui-parts/link'

import React                 from 'react'

import { Link }              from './link.component'

export const NextNavLink = (props) => {
  const BaseLink = createNextNavLink(Link)

  return <BaseLink {...props} />
}
