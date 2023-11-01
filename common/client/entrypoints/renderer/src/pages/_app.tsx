import * as messages      from '../../locales/ru.json'

import { ApolloProvider } from '@apollo/client'

import React              from 'react'
import { IntlProvider }   from 'react-intl'

import { ThemeProvider }  from '@ui/theme'
import { getClient }      from '@shared/data'

const App = ({ Component, pageProps, ...props }) => {
  const client = getClient()

  return (
    <ApolloProvider client={client}>
      <ThemeProvider>
        <IntlProvider locale='ru' defaultLocale='ru' messages={messages}>
          <Component {...pageProps} {...props} />
        </IntlProvider>
      </ThemeProvider>
    </ApolloProvider>
  )
}

export default App
