import { Avatar as AvatarRoot } from '@atls-ui-proto/avatar'
import { AvatarImage }          from '@atls-ui-proto/avatar'
import { AvatarFallback }       from '@atls-ui-proto/avatar'

import React                    from 'react'
import { FC }                   from 'react'
import { useRouter }            from 'next/router'

import { Condition }            from '@ui/condition'
import { Box }                  from '@ui/layout'
import { Link }                 from '@ui/link'
import { theme }                from '@ui/theme'

import { AvatarProps }          from './avatar.interfaces'

export const Avatar: FC<AvatarProps> = ({
  image,
  shape = 'circle',
  size = 74,
  url,
  title,
  color = '',
  children,
  style,
}) => {
  const router = useRouter()

  const handleClick = (event) => {
    event.preventDefault()

    if (url) router.push(url)
  }

  return (
    <Box title={title} style={{ cursor: url ? 'pointer' : 'default' }}>
      <AvatarRoot
        style={{
          width: size,
          height: size,
          backgroundColor: theme.backgrounds?.[color] || 'transparent',
          borderRadius: shape === 'circle' ? theme.radii.full : theme.radii.medium,
          ...style,
        }}
      >
        <Condition match={Boolean(url)}>
          <Link href={url} width='100%' height='100%' onClick={handleClick}>
            <AvatarImage src={image} />
            <Condition match={Boolean(children)}>
              <AvatarFallback>{children}</AvatarFallback>
            </Condition>
          </Link>
        </Condition>
        <Condition match={!url}>
          <AvatarImage src={image} />
          <Condition match={Boolean(children)}>
            <AvatarFallback>{children}</AvatarFallback>
          </Condition>
        </Condition>
      </AvatarRoot>
    </Box>
  )
}
