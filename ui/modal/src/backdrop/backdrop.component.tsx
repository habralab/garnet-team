import styled                     from '@emotion/styled'

import { BackdropElement }        from './backdrop.element'
import { backdropMotionVariants } from './backdrop.motion'
import { baseBackdropStyles }     from './backdrop.styles'

const Backdrop = styled(BackdropElement)(baseBackdropStyles)

Backdrop.defaultProps = {
  variants: backdropMotionVariants,
  animate: 'target',
  initial: 'enter',
  exit: 'exit',
}

export { Backdrop }
