import * as fonts from './fonts'

import { Global } from '@emotion/react'
import { css }    from '@emotion/react'

import React      from 'react'

export const GlobalStyles = () => (
  <Global
    styles={css`
    @font-face {
      font-family: 'Inter';
      src: url(${fonts.InterVariable}) format('truetype');
    }

    body {
      overflow: auto;
    }
  `}
  />
)
