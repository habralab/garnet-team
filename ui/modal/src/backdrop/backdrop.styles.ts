import { CSSObject } from '@emotion/css'

export const baseBackdropStyles: CSSObject = {
  zIndex: -1,
  position: 'fixed',
  bottom: 0,
  top: 0,
  left: 0,
  right: 0,
  backgroundColor: 'rgba(53, 53, 53, 0.5)',
  WebkitTapHighlightColor: 'transparent',
}
