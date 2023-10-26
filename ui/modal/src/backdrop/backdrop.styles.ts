import { styleFn } from 'styled-system'

export const baseBackdropStyles: styleFn = ({ theme }) => ({
  zIndex: -1,
  position: 'fixed',
  bottom: 0,
  top: 0,
  left: 0,
  right: 0,
  backgroundColor: theme.backgrounds.blackTransparent,
  WebkitTapHighlightColor: 'transparent',
})
