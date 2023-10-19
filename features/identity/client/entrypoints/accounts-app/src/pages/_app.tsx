import React            from 'react'
import { IntlProvider } from 'react-intl'

const App = ({ Component, pageProps, ...props }) => (
  <IntlProvider locale='ru' defaultLocale='ru' messages={{}}>
    <Component {...pageProps} {...props} />
  </IntlProvider>
)

export default App
