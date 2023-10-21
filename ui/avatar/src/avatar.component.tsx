import { Avatar as AvatarRoot } from '@atls-ui-proto/avatar'
import { AvatarImage }          from '@atls-ui-proto/avatar'

import React                    from 'react'
import { FC }                   from 'react'
import { useRouter }            from 'next/router'

import { Box }                  from '@ui/layout'
import { theme }                from '@ui/theme'

import { AvatarProps }          from './avatar.interfaces'

export const Avatar: FC<AvatarProps> = ({ image, shape = 'circle', size = 74, url, title }) => {
  const router = useRouter()

  const handleClick = () => url && router.push(url)

  return (
    <Box title={title} style={{ cursor: url ? 'pointer' : 'default' }} onClick={handleClick}>
      <AvatarRoot
        style={{
          width: size,
          height: size,
          backgroundColor: 'transparent',
          borderRadius: shape === 'circle' ? '100%' : theme.radii.medium,
        }}
      >
        <AvatarImage src={image} />
      </AvatarRoot>
    </Box>
  )
}
