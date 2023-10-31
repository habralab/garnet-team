import 'cropperjs/dist/cropper.css'

import * as messages     from '../../locales/ru.json'

import React             from 'react'
import { IntlProvider }  from 'react-intl'

import { ThemeProvider } from '@ui/theme'

const App = ({ Component, pageProps, ...props }) => (
  <ThemeProvider>
    <IntlProvider locale='ru' defaultLocale='ru' messages={messages}>
      <Component {...pageProps} {...props} />
    </IntlProvider>
  </ThemeProvider>
)

export default App
