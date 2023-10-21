import { styleFn } from 'styled-system'

export const shapeLinkStyles: styleFn = ({ theme }) => ({
  fontSize: theme.fontSizes.semiMedium,
  fontFamily: theme.fonts.primary,
  whiteSpace: 'nowrap',
})
