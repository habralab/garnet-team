import { createBaseStyles }       from '@atls-ui-parts/input'
import { createShapeStyles }      from '@atls-ui-parts/input'
import { createAppearanceStyles } from '@atls-ui-parts/input'

import { switchProp }             from 'styled-tools'
import { prop }                   from 'styled-tools'
import { ifProp }                 from 'styled-tools'

const normalSizeStyles = createShapeStyles({
  fontFamily: prop('theme.fonts.primary'),
  fontWeight: 400,
  size: 48,
  fontSize: 15,
  rounding: 10,
  paddingLeft: 16,
  paddingRight: 16,
})

export const baseStyles = createBaseStyles()

export const primaryColorsStyles = ({ theme }) => ({
  ...createAppearanceStyles({
    fontColor: theme.colors.input.primary.default.font,
    backgroundColor: theme.colors.input.primary.default.background,
    borderColor: theme.colors.input.primary.default.border,
  })(),
  '&:hover': {
    ...createAppearanceStyles({
      fontColor: theme.colors.input.primary.hover.font,
      backgroundColor: theme.colors.input.primary.hover.background,
      borderColor: theme.colors.input.primary.hover.border,
    })(),
  },
  '&:focus-within': {
    ...createAppearanceStyles({
      fontColor: theme.colors.input.primary.focus.font,
      backgroundColor: theme.colors.input.primary.focus.background,
      borderColor: theme.colors.input.primary.focus.border,
    })(),
  },
  '&:active': {
    ...createAppearanceStyles({
      fontColor: theme.colors.input.primary.pressed.font,
      backgroundColor: theme.colors.input.primary.pressed.background,
      borderColor: theme.colors.input.primary.pressed.border,
    })(),
  },

  'textarea, input': {
    [`
    &::-webkit-input-placeholder, 
    &:-moz-placeholder, 
    &::-moz-placeholder, 
    &:-ms-input-placeholder, 
    &::-ms-input-placeholder, 
    &::placeholder`]: {
      color: theme.colors.text.lightGrey,
    },
  },
})

export const errorColorsStyles = createAppearanceStyles({
  fontColor: ({ theme }) => theme.colors.input.primary.error.font,
  backgroundColor: ({ theme }) => theme.colors.input.primary.error.background,
  borderColor: ({ theme }) => theme.colors.input.primary.error.border,
})

export const disabledColorsStyles = createAppearanceStyles({
  fontColor: ({ theme }) => theme.colors.input.primary.disabled.font,
  backgroundColor: ({ theme }) => theme.colors.input.primary.disabled.background,
  borderColor: ({ theme }) => theme.colors.input.primary.disabled.border,
})

export const appearanceStyles = ifProp(
  prop('disabled', false),
  disabledColorsStyles,
  ifProp(prop('error', false), errorColorsStyles, primaryColorsStyles)
)

export const shapeStyles = switchProp(prop('size', 'normal'), {
  normal: normalSizeStyles,
})

export const transitionStyles = { transition: '.2s' }
