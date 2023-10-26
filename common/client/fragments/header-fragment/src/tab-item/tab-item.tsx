import styled               from '@emotion/styled'

import { Text }             from '@ui/text'
import { TextProps }        from '@ui/text'

import { transitionStyles } from './tab-item.styles'
import { activeStyles }     from './tab-item.styles'
import { shapeStyles }      from './tab-item.styles'
import { appearanceStyles } from './tab-item.styles'

export const TabItem = styled(Text)<TextProps>(
  appearanceStyles,
  activeStyles,
  shapeStyles,
  transitionStyles
)
