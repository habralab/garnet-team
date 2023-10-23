import { ThemeProvider } from '@ui/theme'
import React             from 'react'
import { IntlProvider }  from 'react-intl'

const App = ({ Component, pageProps, ...props }) => (
  <ThemeProvider>
    <IntlProvider locale='ru' defaultLocale='ru'>
      <Component {...pageProps} {...props} />
    </IntlProvider>
  </ThemeProvider>
)

export default App
