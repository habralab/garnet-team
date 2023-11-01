import { createShapeStyles }   from '@atls-ui-parts/button'
import { createContentStyles } from '@atls-ui-parts/button'

import { styleFn }             from 'styled-system'
import { prop }                from 'styled-tools'
import { switchProp }          from 'styled-tools'
import { ifProp }              from 'styled-tools'

const fillStyles: styleFn = ifProp(prop('fill', false), { width: '100%' })

const eventsStyles: styleFn = ifProp(prop('disabled', false), { cursor: 'not-allowed' })

const mediumPaddingMediumHeightStyles = createShapeStyles({
  paddingLeft: 30,
  paddingRight: 30,
  rounding: 8,
  size: 44,
})

const mediumPaddingMediumHeightWithIconStyles = createShapeStyles({
  paddingLeft: 26,
  paddingRight: 30,
  rounding: 8,
  size: 44,
})

const mediumPaddingSmallHeightStyles = createShapeStyles({
  paddingLeft: 26,
  paddingRight: 26,
  rounding: 8,
  size: 36,
})

const mediumPaddingSmallHeightWithIconStyles = createShapeStyles({
  paddingLeft: 20,
  paddingRight: 26,
  rounding: 8,
  size: 36,
})

const mediumPaddingMicroHeightStyles = createShapeStyles({
  paddingLeft: 0.01,
  paddingRight: 0.01,
  rounding: 0,
  size: 20,
})

const contentStyles = createContentStyles()

const shapeStyles = switchProp(prop('size', 'normal'), {
  normal: ifProp(
    'withIcon',
    mediumPaddingMediumHeightWithIconStyles,
    mediumPaddingMediumHeightStyles
  ),
  small: ifProp('withIcon', mediumPaddingSmallHeightWithIconStyles, mediumPaddingSmallHeightStyles),
  micro: mediumPaddingMicroHeightStyles,
})

export { eventsStyles, fillStyles, contentStyles, shapeStyles }
