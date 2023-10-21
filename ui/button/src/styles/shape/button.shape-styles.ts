import { createShapeStyles }   from '@atls-ui-parts/button'
import { createContentStyles } from '@atls-ui-parts/button'

import { styleFn }             from 'styled-system'
import { prop }                from 'styled-tools'
import { switchProp }          from 'styled-tools'
import { ifProp }              from 'styled-tools'

const fillStyles: styleFn = ifProp(prop('fill', false), { width: '100%' })

const mediumPaddingMediumHeightStyles = createShapeStyles({
  paddingLeft: 30,
  paddingRight: 30,
  rounding: 8,
  size: 44,
})

const contentStyles = createContentStyles()

const shapeStyles = switchProp(prop('size', 'normal'), {
  normal: mediumPaddingMediumHeightStyles,
})

export { fillStyles, contentStyles, shapeStyles }