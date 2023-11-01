import { styleFn }    from 'styled-system'
import { ifProp }     from 'styled-tools'
import { prop }       from 'styled-tools'
import { switchProp } from 'styled-tools'

export const containerBaseStyles: styleFn = () =>
  ({ theme }) => ({
    alignItems: 'center',
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

const shapeNormalStyles: styleFn = () => () => ({
  height: 31,
  width: 'max-content',
  padding: '6px 10px',
})

const shapeSmallStyles: styleFn = () => () => ({
  height: 22,
  width: 'max-content',
  padding: '6px 8px',
})

export const hoverStyles = ifProp(prop('hover', false), appearanceHoverStyles)

export const appearanceStyles = switchProp(prop('variant', 'primary'), {
  primary: appearancePrimaryStyles,
  secondary: appearanceSecondaryStyles,
})

export const shapeStyles = switchProp(prop('size', 'normal'), {
  normal: shapeNormalStyles,
  small: shapeSmallStyles,
})
