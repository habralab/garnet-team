import { createAppearanceStyles } from '@atls-ui-parts/input'

import { ifProp }                 from 'styled-tools'
import { switchProp }             from 'styled-tools'
import { prop }                   from 'styled-tools'

const appearancePrimaryDefaultStyles = createAppearanceStyles({
  fontColor: prop('theme.colors.input.primary.default.font'),
  backgroundColor: prop('theme.colors.input.primary.default.background'),
  borderColor: prop('theme.colors.input.primary.default.border'),
})

const appearancePrimaryHoverStyles = createAppearanceStyles({
  fontColor: prop('theme.colors.input.primary.hover.font'),
  backgroundColor: prop('theme.colors.input.primary.hover.background'),
  borderColor: prop('theme.colors.input.primary.hover.border'),
})

const appearancePrimaryPressedStyles = createAppearanceStyles({
  fontColor: prop('theme.colors.input.primary.pressed.font'),
  backgroundColor: prop('theme.colors.input.primary.pressed.background'),
  borderColor: prop('theme.colors.input.primary.pressed.border'),
})

const appearancePrimaryFocusStyles = createAppearanceStyles({
  fontColor: prop('theme.colors.input.primary.focus.font'),
  backgroundColor: prop('theme.colors.input.primary.focus.background'),
  borderColor: prop('theme.colors.input.primary.focus.border'),
})

const appearancePrimaryDisabledStyles = createAppearanceStyles({
  fontColor: prop('theme.colors.input.primary.disabled.font'),
  backgroundColor: prop('theme.colors.input.primary.disabled.background'),
  borderColor: prop('theme.colors.input.primary.disabled.border'),
})

const appearancePrimaryErrorStyles = createAppearanceStyles({
  fontColor: prop('theme.colors.input.primary.error.font'),
  backgroundColor: prop('theme.colors.input.primary.error.background'),
  borderColor: prop('theme.colors.input.primary.error.border'),
})

const appearanceStyles = switchProp(prop('variant', 'primary'), {
  primary: ifProp(
    prop('disabled', false),
    appearancePrimaryDisabledStyles,
    ifProp(
      prop('error', false),
      appearancePrimaryErrorStyles,
      ifProp(
        prop('pressed', false),
        appearancePrimaryPressedStyles,
        ifProp(
          prop('focus', false),
          appearancePrimaryFocusStyles,
          ifProp(prop('hover', false), appearancePrimaryHoverStyles, appearancePrimaryDefaultStyles)
        )
      )
    )
  ),
})

export { appearanceStyles }
