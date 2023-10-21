import { createShapeStyles } from '@atls-ui-parts/input'

import { switchProp }        from 'styled-tools'
import { prop }              from 'styled-tools'

const normalSizeStyles = createShapeStyles({
  fontFamily: prop('theme.fonts.primary'),
  fontWeight: 400,
  size: 48,
  fontSize: 15,
  rounding: 10,
  paddingLeft: 16,
  paddingRight: 16,
})

export const shapeStyles = switchProp(prop('size', 'normal'), {
  normal: normalSizeStyles,
})
