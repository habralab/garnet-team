import React                from 'react'
import { FC }               from 'react'
import { FormattedMessage } from 'react-intl'
import { useRouter }        from 'next/router'

import { Condition }        from '@ui/condition'
import { ArrowLeftIcon }    from '@ui/icon'
import { Box }              from '@ui/layout'
import { Column }           from '@ui/layout'
import { Layout }           from '@ui/layout'
import { Link }             from '@ui/link'
import { Text }             from '@ui/text'

import { TitleProps }       from './title.interfaces'

const BackJsx: FC = () => (
  <Box alignItems='center'>
    <ArrowLeftIcon width={14} height={14} color='currentColor' />
    <Layout flexBasis={4} flexShrink={0} />
    <Text fontSize='normal' color='currentColor'>
      <FormattedMessage id='shared_ui.back' />
    </Text>
  </Box>
)

export const Title: FC<TitleProps> = ({ backUrl, children, hasBack }) => {
  const router = useRouter()

  const handleClickBack = (event) => {
    event.preventDefault()

    if (backUrl) {
      router.push(backUrl)

      return
    }

    router.back()
  }

  return (
    <Column height='max-content'>
      <Layout flexBasis={32} flexShrink={0} />
      <Column minHeight={63}>
        <Text fontSize='extraHuge' fontWeight='superBold' color='text.secondary'>
          {children}
        </Text>
        <Condition match={Boolean(hasBack)}>
          <Layout flexBasis={10} flexShrink={0} />
          <Box>
            <Link href={backUrl || '/'} onClick={handleClickBack}>
              <BackJsx />
            </Link>
          </Box>
        </Condition>
      </Column>
      <Layout flexBasis={32} flexShrink={0} />
    </Column>
  )
}
