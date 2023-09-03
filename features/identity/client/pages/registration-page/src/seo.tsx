import React       from 'react'
import { Helmet }  from 'react-helmet'
import { useIntl } from 'react-intl'

import messages    from './messages'

export const Seo = () => {
  const intl = useIntl()

  const title = intl.formatMessage(messages.title)
  const description = intl.formatMessage(messages.description)

  return (
    <Helmet
      htmlAttributes={{ lang: intl.locale }}
      title={title}
      titleTemplate={`%s | ${title}`}
      meta={[
        {
          name: 'description',
          content: description,
        },
        {
          property: 'og:title',
          content: title,
        },
        {
          property: 'og:description',
          content: description,
        },
        {
          property: 'og:type',
          content: 'website',
        },
        {
          name: 'twitter:card',
          content: 'summary',
        },
        {
          name: 'twitter:title',
          content: title,
        },
        {
          name: 'twitter:description',
          content: description,
        },
      ]}
    />
  )
}
