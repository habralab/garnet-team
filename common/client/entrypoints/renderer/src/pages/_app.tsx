import * as messages     from '../../locales/ru.json'

import { KratosClientProvider } from '@atls/next-identity-integration'

import React                    from 'react'
import { IntlProvider }         from 'react-intl'

import { ThemeProvider }        from '@ui/theme'
import { returnToSettingsUrl }  from '@shared/kratos-client'
import { kratosClient }         from '@shared/kratos-client'

const App = ({ Component, pageProps, ...props }) => (
  <ThemeProvider>
    <IntlProvider locale='ru' defaultLocale='ru' messages={messages}>
      <KratosClientProvider value={{ returnToSettingsUrl, kratosClient }}>
        <Component {...pageProps} {...props} />
      </KratosClientProvider>
    </IntlProvider>
  </ThemeProvider>
)

export default App
