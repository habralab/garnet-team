import styled                from '@emotion/styled'

import { InputElementProps } from './input.interfaces'
import { transitionStyles }  from './input.styles'
import { shapeStyles }       from './input.styles'
import { baseStyles }        from './input.styles'
import { appearanceStyles }  from './input.styles'

export const InputElement = styled.div<InputElementProps>(
  baseStyles,
  shapeStyles,
  appearanceStyles,
  transitionStyles
)
