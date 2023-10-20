import { styleFn } from 'styled-system'

export const transitionStyles: styleFn = () => ({
  transition: '0.3s',
})

export const activeLinkStyles: styleFn = ({ active, theme }) =>
  active && {
    color: theme.colors.text.accent,
  }
