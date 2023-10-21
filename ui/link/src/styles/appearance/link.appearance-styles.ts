import { styleFn }    from 'styled-system'
import { prop }       from 'styled-tools'
import { switchProp } from 'styled-tools'

const defaultVariantAppearanceStyles: styleFn = ({ theme, keep }) =>
  !keep && {
    color: theme.colors.text.accent,
    '&:hover': {
      color: theme.colors.text.accent,
    },
  }

export const appearanceStyles = switchProp(prop('variant', 'default'), {
  default: defaultVariantAppearanceStyles,
})
