import * as React                                from 'react'

import * as theme                                from './theme'

import { ThemeProvider as ProtoThemeProvider }   from '@atls-ui-proto/theme'
import { ThemeProvider as EmotionThemeProvider } from '@emotion/react'

import { GlobalStyles }                          from './global.styles'

const CustomTheme = ({ children }) => (
  <>
    <GlobalStyles />
    <EmotionThemeProvider theme={theme}>{children}</EmotionThemeProvider>
  </>
)

export const ThemeProvider = ({ children }) => (
  <ProtoThemeProvider children={<CustomTheme children={children} />} />
)
