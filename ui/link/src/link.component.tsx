import styled               from '@emotion/styled'
import { createBaseStyles } from '@atls-ui-parts/link'

import React                from 'react'

import { Text }             from '@ui/text'

import { BaseLinkProps }    from './link.interfaces'
import { transitionStyles } from './styles'
import { activeLinkStyles } from './styles'
import { shapeLinkStyles }  from './styles'
import { appearanceStyles } from './styles'

const LinkElement = styled(Text.withComponent('a'))<BaseLinkProps>(
  createBaseStyles(),
  appearanceStyles,
  shapeLinkStyles,
  activeLinkStyles,
  transitionStyles
)

const basePath = '/app'

export const Link = ({ children, href, ...props }) => (
  <LinkElement href={`${basePath}${href || props.path}`} {...props}>
    {children}
  </LinkElement>
)
