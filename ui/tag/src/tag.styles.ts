import { styleFn }    from 'styled-system'
import { ifProp }     from 'styled-tools'
import { prop }       from 'styled-tools'
import { switchProp } from 'styled-tools'

export const containerBaseStyles: styleFn = () =>
  ({ theme }) => ({
    height: 31,
    alignItems: 'center',
    padding: '6px 10px',
    border: theme.borders.blue,
    borderRadius: theme.radii.medium,
  })

const appearancePrimaryStyles: styleFn = () =>
  ({ theme }) => ({
    backgroundColor: theme.backgrounds.lightBlue,
  })

const appearanceSecondaryStyles: styleFn = () =>
  ({ theme }) => ({
    backgroundColor: theme.backgrounds.white,
  })

const appearanceHoverStyles: styleFn = () =>
  ({ theme }) => ({
    '&:hover': {
      backgroundColor: theme.backgrounds.darkBlue,
    },
  })

export const hoverStyles = ifProp(prop('hover', false), appearanceHoverStyles)

export const appearanceStyles = switchProp(prop('variant', 'primary'), {
  primary: appearancePrimaryStyles,
  secondary: appearanceSecondaryStyles,
})
