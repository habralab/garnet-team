import * as fonts        from './fonts'

import { Global }        from '@emotion/react'
import { css }           from '@emotion/react'

import React             from 'react'

import { CropperStyles } from './cropperjs'

export const GlobalStyles = () => (
  <Global
    styles={css`
      @font-face {
        font-family: 'Inter';
        src: url(${fonts.InterVariable}) format('truetype');
      }

      body {
        overflow: auto;
        position: relative;
      }

      #custom-scrollbar {
        scrollbar-color: rgba(53, 53, 53, 0.1) transparent;

        &::-webkit-scrollbar {
          width: 4px;
          height: 4px;
        }

        &::-webkit-scrollbar-thumb {
          background: rgba(53, 53, 53, 0.1);
        }

        &::-webkit-scrollbar-track {
          background: transparent;
        }
      }

      ${CropperStyles}
    `}
  />
)
