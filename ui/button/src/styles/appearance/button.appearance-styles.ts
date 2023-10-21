import { createAppearanceStyles } from '@atls-ui-parts/button'

import { ifProp }                 from 'styled-tools'
import { switchProp }             from 'styled-tools'
import { prop }                   from 'styled-tools'

const appearancePrimaryDefaultStyles = createAppearanceStyles({
  fontColor: prop('theme.colors.button.primary.default.font'),
  backgroundColor: prop('theme.colors.button.primary.default.background'),
  borderColor: prop('theme.colors.button.primary.default.border'),
})

const appearancePrimaryHoverStyles = createAppearanceStyles({
  fontColor: prop('theme.colors.button.primary.hover.font'),
  backgroundColor: prop('theme.colors.button.primary.hover.background'),
  borderColor: prop('theme.colors.button.primary.hover.border'),
})

const appearancePrimaryPressedStyles = createAppearanceStyles({
  fontColor: prop('theme.colors.button.primary.pressed.font'),
  backgroundColor: prop('theme.colors.button.primary.pressed.background'),
  borderColor: prop('theme.colors.button.primary.pressed.border'),
})

const appearancePrimaryDisabledStyles = createAppearanceStyles({
  fontColor: prop('theme.colors.button.primary.disabled.font'),
  backgroundColor: prop('theme.colors.button.primary.disabled.background'),
  borderColor: prop('theme.colors.button.primary.disabled.border'),
})

const appearanceSecondaryDefaultStyles = createAppearanceStyles({
  fontColor: prop('theme.colors.button.secondary.default.font'),
  backgroundColor: prop('theme.colors.button.secondary.default.background'),
  borderColor: prop('theme.colors.button.secondary.default.border'),
})

const appearanceSecondaryHoverStyles = createAppearanceStyles({
  fontColor: prop('theme.colors.button.secondary.hover.font'),
  backgroundColor: prop('theme.colors.button.secondary.hover.background'),
  borderColor: prop('theme.colors.button.secondary.hover.border'),
})

const appearanceSecondaryPressedStyles = createAppearanceStyles({
  fontColor: prop('theme.colors.button.secondary.pressed.font'),
  backgroundColor: prop('theme.colors.button.secondary.pressed.background'),
  borderColor: prop('theme.colors.button.secondary.pressed.border'),
})

const appearanceSecondaryDisabledStyles = createAppearanceStyles({
  fontColor: prop('theme.colors.button.secondary.disabled.font'),
  backgroundColor: prop('theme.colors.button.secondary.disabled.background'),
  borderColor: prop('theme.colors.button.secondary.disabled.border'),
})

const appearanceStyles = switchProp(prop('variant', 'primary'), {
  primary: ifProp(
    prop('disabled', false),
    appearancePrimaryDisabledStyles,
    ifProp(
      prop('pressed', false),
      appearancePrimaryPressedStyles,
      ifProp(prop('hover', false), appearancePrimaryHoverStyles, appearancePrimaryDefaultStyles)
    )
  ),
  secondary: ifProp(
    prop('disabled', false),
    appearanceSecondaryDisabledStyles,
    ifProp(
      prop('pressed', false),
      appearanceSecondaryPressedStyles,
      ifProp(prop('hover', false), appearanceSecondaryHoverStyles, appearanceSecondaryDefaultStyles)
    )
  ),
})

export { appearanceStyles }
