import { Avatar as AvatarRoot } from '@atls-ui-proto/avatar'
import { AvatarImage }          from '@atls-ui-proto/avatar'

import React                    from 'react'
import { FC }                   from 'react'

import { Condition }            from '@ui/condition'
import { Box }                  from '@ui/layout'
import { NextLink }             from '@ui/link'
import { theme }                from '@ui/theme'

import { AvatarProps }          from './avatar.interfaces'

export const Avatar: FC<AvatarProps> = ({ image, shape = 'circle', size = 74, url, title }) => (
  <Box title={title} style={{ cursor: url ? 'pointer' : 'default' }}>
    <AvatarRoot
      style={{
        width: size,
        height: size,
        backgroundColor: 'transparent',
        borderRadius: shape === 'circle' ? theme.radii.full : theme.radii.medium,
      }}
    >
      <Condition match={Boolean(url)}>
        <NextLink path={url} href={url} width='100%' height='100%'>
          <AvatarImage src={image} />
        </NextLink>
      </Condition>
      <Condition match={!url}>
        <AvatarImage src={image} />
      </Condition>
    </AvatarRoot>
  </Box>
)
