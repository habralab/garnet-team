import * as messages            from '../../locales/ru.json'

import { KratosClientProvider } from '@atls/next-identity-integration'

import React                    from 'react'
import { IntlProvider }         from 'react-intl'

import { ThemeProvider }        from '@ui/theme'
import { returnToSettingsUrl }  from '@shared/kratos-client'
import { kratosClient }         from '@shared/kratos-client'

const App = ({ Component, pageProps, ...props }) => (
  <IntlProvider locale='ru' defaultLocale='ru' messages={messages}>
    <ThemeProvider>
      <KratosClientProvider value={{ kratosClient, returnToSettingsUrl }}>
        <Component {...pageProps} {...props} />
      </KratosClientProvider>
    </ThemeProvider>
  </IntlProvider>
)

export default App
