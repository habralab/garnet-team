import React            from 'react'
import { IntlProvider } from 'react-intl'
import * as messages    from '../../locales/ru.json'

import { ThemeProvider } from '@ui/theme'

const App = ({ Component, pageProps, ...props }) => (
  <IntlProvider locale='ru' defaultLocale='ru' messages={messages}>
    <ThemeProvider>
      <Component {...pageProps} {...props} />
    </ThemeProvider>
  </IntlProvider>
)

export default App
