import * as messages            from '../../locales/ru.json'

import { ApolloProvider }       from '@apollo/client'
import { KratosClientProvider } from '@atls/next-identity-integration'

import React                    from 'react'
import { IntlProvider }         from 'react-intl'

import { SessionProvider }      from '@stores/session'
import { ThemeProvider }        from '@ui/theme'
import { getClient }            from '@shared/data'
import { returnToSettingsUrl }  from '@shared/kratos-client'
import { kratosClient }         from '@shared/kratos-client'

const App = ({ Component, pageProps, ...props }) => {
  const client = getClient()

  return (
    <ApolloProvider client={client}>
      <ThemeProvider>
        <IntlProvider locale='ru' defaultLocale='ru' messages={messages}>
          <KratosClientProvider value={{ returnToSettingsUrl, kratosClient }}>
            <SessionProvider>
              <Component {...pageProps} {...props} />
            </SessionProvider>
          </KratosClientProvider>
        </IntlProvider>
      </ThemeProvider>
    </ApolloProvider>
  )
}

export default App
