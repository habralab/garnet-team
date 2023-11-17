import { CSSObject } from '@emotion/styled'

import { system }    from 'styled-system'

export const textareaElementStyles: CSSObject = {
  padding: 16,
  flexDirection: 'column',
}

export const rawTextareaStyles: CSSObject = {
  height: '100%',
  resize: 'none',
}

export const textareaHeightStyles = system({
  height: {
    property: 'height',
    transform: (value) => (typeof value === 'number' ? `${value}px` : value),
  },
})
