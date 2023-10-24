import { styleFn } from 'styled-system'

export const shapeStyles: styleFn = ({ theme }) => ({
  fontSize: theme.fontSizes.medium,
  fontWeight: theme.fontWeights.medium,
  fontFamily: theme.fonts.primary,
  whiteSpace: 'nowrap',
  textDecoration: 'none',
  cursor: 'pointer',
})

export const transitionStyles: styleFn = () => ({
  transition: '0.3s',
})

export const activeStyles: styleFn = ({ active, theme }) =>
  active && {
    fontWeight: `${theme.fontWeights.bold} !important`,
    color: theme.colors.text.accent,

    '&:hover': {
      color: theme.colors.text.accent,
    },
  }

export const appearanceStyles: styleFn = ({ theme }) => ({
  color: theme.colors.text.secondary,

  '&:hover': {
    color: theme.colors.text.gray,
  },
})
